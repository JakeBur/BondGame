//Author : Colin + Jamo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Linq;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class PlayerController : MonoBehaviour
{
    public struct Inputs
    {
        public bool dash; //Dash and interact are used for same button press, possibly refactor?
        public bool interact;
        public bool basicAttack;
        public bool heavyAttack;
        public Vector3 moveDirection;
        public Vector2 rawDirection;

        public bool usingMouse;
        public Vector2 mousePos;

    }
    public Inputs inputs;

    // COMPONENTS
    public PlayerStateMachine fsm => GetComponent<PlayerStateMachine>();
    public PlayerAnimator animator => GetComponent<PlayerAnimator>();
    //public PlayerStats stats => GetComponent<PlayerStats>();
    public StatManager stats => GetComponent<StatManager>();
    private CharacterController charController;
    private NavMeshAgent agent => GetComponent<NavMeshAgent>();
    private Rigidbody rb => GetComponent<Rigidbody>();

    [Header("Relics")]
    public List<RelicStats> Relics = new List<RelicStats>();

    [Header("Dialog Manager")]
    public bool inCharacterDialog;
    public List<GameObject> interactableObjects = new List<GameObject>();
    [HideInInspector]
    public DialogueManager dialogueManager;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    private bool isPaused = false;

    [Header("Items")]
    public int goldCount = 0;
    [HideInInspector]
    public GameObject fruit;

    [Header("Movement")]
    public bool isoMovement = true;
    private Vector3 gravity;
    private float crouchModifier = 1;
    public float isoSpeedADJ = 0f;
    public float currSpeed;
    public bool inStandby = false; //for standby state

    [Header("Dash")]
    //[HideInInspector]
    public bool isDashing = false;
    //[HideInInspector]
    public Vector3 facingDirection;
    //[HideInInspector]
    public Vector3 lastMoveVec;
    //[HideInInspector]
    public Vector3 movementVector;
    private float dashStart = 2;
    private int dashCount = 0;
    
    [Header("Environment Context")]
    public bool nearInteractable = false;
    
    [Header("Creature Context")]
    public bool hasSwapped;
    public Transform backFollowPoint;
    [HideInInspector]
    public GameObject wildCreature = null;
    [HideInInspector]
    public GameObject currCreature;
    [HideInInspector]
    public GameObject swapCreature;
    [HideInInspector]
    public GameObject interactableObject;
    [HideInInspector]
    public CreatureAIContext currCreatureContext;
    public CooldownSystem cooldownSystem => GetComponent<CooldownSystem>();
    // public bool didWhistle;

    [Header("Combat")]
    public bool inCombat;
    public bool isAttacking = false;
    public bool isHit;
    public Vector3 attackMoveVec;
    public Canvas reticle;
    private Vector3 directionTowardsMouse;

    [Header("PlayerInputs")]
    public PlayerInput playerInputs;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    [Serializable]
    public struct HitBoxes
    {
        public GameObject slash0;
        public GameObject slash1;
        public GameObject slash2;
        public GameObject slash3;
        public GameObject slash4;
        public GameObject heavy;

    }

    public HitBoxes hitBoxes;

    // private void OnEnable()
    // {
    //     InputUser.onChange += OnInputDeviceChanged;
    // }

    // private void OnDisable()
    // {
    //     InputUser.onChange -= OnInputDeviceChanged; 
    // }
    
    // private void OnInputDeviceChanged(InputUser user, InputUserChange change, InputDevice device)
    // {
    //     Debug.Log("user : " + user  + " change : " + change + " device " + device);
    //     if (change == InputUserChange.ControlSchemeChanged) {

    //     }
    // }

    void Start()
    {
        charController = GetComponent<CharacterController>();
        dashStart = Time.time;
        animator.ResetAllAttackAnims();
        inputs.usingMouse = true;
    }

    
    
    private void Update() 
    { 
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(inputs.mousePos);
        int layerMask = 1 << 10;

        // If the raycast hits the ground
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 direction = hit.point - reticle.transform.position;
            directionTowardsMouse = Vector3.RotateTowards(reticle.transform.forward, direction, float.MaxValue, float.MaxValue);

            // Rotates reticle in direction of mouse pos
            reticle.transform.rotation = Quaternion.LookRotation(new Vector3(directionTowardsMouse.x, 0, directionTowardsMouse.z));
        }
    }

    // MOVEMENT FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////////

    public void doMovement(float movementModifier)
    {

        //TODO: Player should never be in the air
        // Detects if player is in the air
        if(!charController.isGrounded)
        {
            gravity += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravity = Vector3.zero;
        }

        // Set movement and lastMove vectors to direction player is facing
        if(isDashing) 
        {   
            if(lastMoveVec == Vector3.zero) 
            {
                lastMoveVec = facingDirection;
            }
            movementVector = lastMoveVec * Time.deltaTime * movementModifier * crouchModifier;
        }
        else if(isAttacking) 
        {   
            //moves player in direction of click
            movementVector = attackMoveVec * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
        }
        else 
        {
            // Normal movement
            movementVector = inputs.moveDirection * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
            lastMoveVec = inputs.moveDirection;
        }
        
        rb.velocity = Vector3.zero;
        agent.Move(movementVector);

        animator.Move(movementVector);
    }


    // Rotates the player
    public void doRotation(float rotationModifier)
    {
        if(inputs.rawDirection != Vector2.zero)
        {
            if(isAttacking)
            {//TODO: CHANGE LATER, DONT HARDCODE TURN SPEED AS 14
                 transform.forward = Vector3.Slerp(transform.forward, lastMoveVec, Time.deltaTime * 14f * rotationModifier);
            }
            else
            {
                transform.forward = Vector3.Slerp(transform.forward, inputs.moveDirection, Time.deltaTime * 14f * rotationModifier);
            }
        }
    }

    // Sets the player's rotation to specified vec
    public void setRotation(Vector3 vec)
    {
        transform.forward = vec;
    }

    
    // Input Functions ///////////////////////////////////////////////////////////////////////////////////////

    // WASD and Joystick
    private void OnMovement(InputValue value)
    {
        // Takes 2D movement vector and converts to 3D
        inputs.rawDirection = value.Get<Vector2>();
        inputs.rawDirection.Normalize();
        inputs.rawDirection.y *= isoSpeedADJ;

        inputs.moveDirection = new Vector3(inputs.rawDirection.x, 0, inputs.rawDirection.y);

        // Rotates inputs to convert from world space to screen space
        if(isoMovement)
        {
            inputs.moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * inputs.moveDirection;
        }

        // Sets facingDirection to movement direction
        if(inputs.moveDirection != Vector3.zero)
        {
            facingDirection = inputs.moveDirection;
        }
    }

    // Mouse position
    private void OnMousePos(InputValue value)
    {
        inputs.usingMouse = true;
        inputs.mousePos = value.Get<Vector2>();
    }
    
    public GameObject updateInteractDistances()
    {
        if(interactableObjects.Count < 2) return interactableObjects[0];
        foreach(GameObject interactable in interactableObjects)
        {
            interactable.GetComponent<InteractableBase>().distance = 
                Vector3.Distance(interactable.GetComponent<InteractableBase>().transform.position, gameObject.transform.position);
        }
        interactableObjects.Sort((x, y) => (x.GetComponent<InteractableBase>().distance).CompareTo(y.GetComponent<InteractableBase>().distance));
        return interactableObjects[0];
    }

    public void displayInteractUI()
    {
        if(interactableObjects.Count > 0)
        {
            GameObject closestInteractable = interactableObjects[0];
            if(closestInteractable.transform.GetComponentInParent<CreatureAIContext>())
            {
                if(!closestInteractable.transform.GetComponentInParent<CreatureAIContext>().creatureFrozen)
                {
                    if( closestInteractable.GetComponent<InteractableBase>().showUI)
                    {
                        PersistentData.Instance.hudManager.ShowInteractPrompt();
                    }
                }
            }
            else if(closestInteractable.GetComponent<PotionInteractable>())
            {
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(closestInteractable.GetComponent<PotionInteractable>().relicStats,
                                                                                         closestInteractable.GetComponent<PotionInteractable>().cost);
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                if( closestInteractable.GetComponent<InteractableBase>().showUI)
                {
                    PersistentData.Instance.hudManager.ShowInteractPrompt();
                }
            }
            else if(closestInteractable.GetComponent<AcornBagInteractable>())
            {
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(closestInteractable.GetComponent<AcornBagInteractable>().relicStats,
                                                                                         closestInteractable.GetComponent<AcornBagInteractable>().cost);
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                if( closestInteractable.GetComponent<InteractableBase>().showUI)
                {
                    PersistentData.Instance.hudManager.ShowInteractPrompt();
                }
            }
            else if(closestInteractable.GetComponent<RelicInteractable>())
            {
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(closestInteractable.GetComponent<RelicInteractable>().relicStats,
                                                                                         closestInteractable.GetComponent<RelicInteractable>().cost);
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                if( closestInteractable.GetComponent<InteractableBase>().showUI)
                {
                    PersistentData.Instance.hudManager.ShowInteractPrompt();
                }
            }
            else
            {
                if(closestInteractable.GetComponent<InteractableBase>().showUI)
                {
                    PersistentData.Instance.hudManager.ShowInteractPrompt();
                }
            }

        }
    }
    
    // F and North face button
    private void OnInteract()
    {
        // For dialog scenes
        if(inCharacterDialog)
        {
            dialogueManager?.OnInteract();
        }
        // If interactable is close by
        else if(interactableObjects.Count > 0)
        {
            InteractableBase tempBase = null;
            GameObject tempObj = null;
            float closestDist = 20;

            // Find object that is the closest
            tempObj = interactableObjects[0];
            tempBase = tempObj.GetComponent<InteractableBase>();
            // Do the action
            tempBase.DoInteract();
            // If object is removed after interact
            if(tempBase.removeOnInteract)
            {
                interactableObjects.Remove(tempObj);
            }
            // Hide the prompt if no interactables are near
            if(interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.HideIntereactPrompt();
            }
        }
    }

    // Space and South button
    private void OnDash()
    {
        if ( inStandby )
        {
            return;
        }

        // cant dash until more time than dash delay has elapsed,
        if(Time.time > dashStart + stats.getStat(ModiferType.DASH_COOLDOWN))
        {
            //takes dash start time
            dashStart = Time.time;
            dashCount++;
            inputs.dash = true;
        }
        //if you have dashed once and are not past delay, you can dash a second time
        else if(dashCount >= 1 )
        {
            dashCount = 0;
            inputs.dash = true;          
                
        }   
    }

    // X
    private void OnSwap()
    {
        if( swapCreature != null  && !inStandby )
        {
            var temp = currCreature;

            // currCreature.GetComponent<CreatureAIContext>().agent.Warp(Vector3.zero);                // Warp to off map
            // swapCreature.GetComponent<CreatureAIContext>().agent.Warp(backFollowPoint.position);    // Move new creature into position
            // swapCreature.transform.position = backFollowPoint.position;
            currCreatureContext.creatureReticle.SetActive(false);
            currCreature = swapCreature;                                                            // Actual Swap
            currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
            currCreatureContext.creatureReticle.SetActive(true);
            // currCreatureContext.isInPlayerRadius = false;
            // currCreatureContext.isActive = true;

            swapCreature = temp;

            // swapCreature.GetComponent<CreatureAIContext>().isActive = false;
            
            //Update the creature's enthusiasm bars
            // swapCreature.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();
            // currCreature.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();

            hasSwapped = !hasSwapped;
            if(hasSwapped)
            {
                PersistentData.Instance.hudManager.abilityId1 = 100;
                PersistentData.Instance.hudManager.abilityId2 = 101;
            }
            else 
            {
                PersistentData.Instance.hudManager.abilityId1 = 0;
                PersistentData.Instance.hudManager.abilityId2 = 1;
            }
            
            PersistentData.Instance.hudManager.UpdateCreatureUI();                // UI Update

            SFXPlayer.PlayOneShot(SFX.CreatureSwapSFX, transform.position);                      // Sound
        }
        

    }


    //Slash (X)
    private void OnAttack1()
    {
        if ( !inStandby )
        {
            inputs.basicAttack = true;
        }
    }


    //Holding X
    private void OnHeavyAttack(InputValue value)
    {
        if ( inStandby )
        {
            return;
        }

        float val = value.Get<float>();

        if(val == 1)
        {
            inputs.heavyAttack = true;
        }
        else
        {
            inputs.heavyAttack = false;
        }
        
    }


    //creature ability 1 (Y)
    private void OnAttack2()
    {
        // var id = currCreatureContext.CD.abilities[0].id;
        // var cooldownDuration = currCreatureContext.CD.abilities[0].cooldownDuration;
        if( currCreature != null && !inStandby )
        {
            currCreatureContext.isAbilityTriggered = true;
            currCreatureContext.lastTriggeredAbility = 0;
            currCreatureContext.wentToPlayerForAbility = false;
        }

    }  


    //creature ability 2 (B)
    private void OnAttack3()
    {
        if( currCreature != null && !inStandby )
        {
            currCreatureContext.isAbilityTriggered = true;
            currCreatureContext.lastTriggeredAbility = 1;
            currCreatureContext.wentToPlayerForAbility = false;
        }
    }

    private void OnOpenStats()
    {
        if ( inStandby )
        {
            return;
        }

        var ui = PersistentData.Instance.StatUI;
        ui.SetActive(!ui.activeInHierarchy);
        //PersistentData.Instance.StatUI.GetComponent<StatUIFunctions>().UpdateCreatureStats(1);
       


        
        SFXPlayer.PlayOneShot(SFX.MenuOpenSFX, transform.position);

    }

    private void OnCrouch()
    {
        if ( inStandby )
        {
            return;
        }

        // If standing, then crouch
        if(crouchModifier == 1f)
        {
            crouchModifier = .5f;
            animator.Crouch( true );
        }
        // If crouched, then stand up
        else
        {
            crouchModifier = 1f;
            animator.Crouch( false );
        }
    }

    private void OnWhistle()
    {
        if ( inStandby )
        {
            return;
        }

        // Debug.Log("WHISTLED");
        SFXPlayer.PlayOneShot(SFX.PlayerWhistleSFX, transform.position);

        if(currCreature == null) return;
        
        // didWhistle = true;

        currCreatureContext.attention += 100;
        
        if(currCreatureContext.attention >150)
        {
            currCreatureContext.attention = 150;
        }
        if(swapCreature != null)
        {
            swapCreature.GetComponent<CreatureAIContext>().attention += 100;
            if(swapCreature.GetComponent<CreatureAIContext>().attention >150)
            {
                swapCreature.GetComponent<CreatureAIContext>().attention = 150;
            }
        }
        
    }

    private void OnPause()
    {
        PersistentData.Instance.pauseUI.ProcessKeyPress();
    }

    private void OnFruitSpawn()
    {
        var temp = Instantiate(fruit, transform.position, Quaternion.identity);
        temp.GetComponent<Fruit>().droppedByPlayer = true;
    }

    // Action Functions /////////////////////////////////////////////////////////////////////////////////////////////////

    public void befriendCreature(CreatureAIContext _context, string creatureType)
    {
        if(currCreature == null)
        {
            wildCreature.GetComponent<CreatureAIContext>().isWild = false;
            currCreature = wildCreature;
            currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
            //currCreatureContext.isActive = true;
            ApplyCreatureRelics(currCreatureContext.creatureStats.statManager);
            // HERMAN TODO: Place this in playerAnimator
            wildCreature.GetComponentInChildren<ParticleSystem>().Play();               //PLAYS HEARTS, NEED TO CHANGE SO IT WORKS WITH MULTIPLE P-SYSTEMS
            SFX.PlayCreatureBefriendSFX(creatureType, currCreature.transform.position);
            PersistentData.Instance.hudManager.UpdateCreatureUI();
            currCreatureContext.creatureReticle.SetActive(true);
            SetCombatState(inCombat);                                                   // Tells creature if it's in combat
        }
        // Already have 1 creature 
        else if(swapCreature == null)
        {
            wildCreature.GetComponent<CreatureAIContext>().isWild = false;                                  // Marks wild creature as not wild
            swapCreature = wildCreature;                                                                    // Stores wild creature as swap creature
            // swapCreature.GetComponent<CreatureAIContext>().agent.Warp(Vector3.zero);                        // Warps off map
            // swapCreature.GetComponent<CreatureAIContext>().isActive = false;

            ApplyCreatureRelics(swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager);  // Apply relics to the creature
            // HERMAN TODO: Place this in playerAnimator
            wildCreature.GetComponentInChildren<ParticleSystem>().Play();               //PLAYS HEARTS, NEED TO CHANGE SO IT WORKS WITH MULTIPLE P-SYSTEMS
            SFX.PlayCreatureBefriendSFX(creatureType, currCreature.transform.position);
            PersistentData.Instance.hudManager.UpdateCreatureUI();

            SetCombatState(inCombat);                                                   // Tells creature if it's in combat
        } 
        else 
        {
            PersistentData.Instance.hudManager.ShowCreatureBefriendUI(_context, creatureType);
        }

    }

    public void swapCreatureOne(CreatureAIContext _context, string creatureType)
    {
        currCreatureContext.isWild = true;
        currCreatureContext.creatureReticle.SetActive(false);
        currCreatureContext = _context;
        currCreatureContext.isWild = false;
        currCreature = _context.gameObject;
        ApplyCreatureRelics(currCreatureContext.creatureStats.statManager);
        currCreatureContext.creatureReticle.SetActive(true);
        // HERMAN TODO: Place this in playerAnimator
        currCreature.GetComponentInChildren<ParticleSystem>().Play();               //PLAYS HEARTS, NEED TO CHANGE SO IT WORKS WITH MULTIPLE P-SYSTEMS
        SFX.PlayCreatureBefriendSFX(creatureType, transform.position);
        PersistentData.Instance.hudManager.UpdateCreatureUI();

        SetCombatState(inCombat);     
    }

    public void swapCreatureTwo(CreatureAIContext _context, string creatureType)
    {
        CreatureAIContext swapContext = swapCreature.GetComponent<CreatureAIContext>();
        swapContext.isWild = true;
        swapContext = _context;
        swapCreature = _context.gameObject;
        
        swapContext.isWild = false;
        ApplyCreatureRelics(swapContext.creatureStats.statManager);
        // HERMAN TODO: Place this in playerAnimator
        swapContext.GetComponentInChildren<ParticleSystem>().Play();               //PLAYS HEARTS, NEED TO CHANGE SO IT WORKS WITH MULTIPLE P-SYSTEMS
        
        PersistentData.Instance.hudManager.UpdateCreatureUI();

        SetCombatState(inCombat);  
    }

    public void ApplyCreatureRelics(StatManager _statManager)
    {
        // Sorts through relics list and applies it to the creature
        foreach(RelicStats relic in Relics)
        {
            _statManager.AddRelic(relic.creatureModifiers);
        }
    }

    public void PutOnCD()
    {
        // hasSwapped changes state based on what creature is out
        // Second creature
        if (hasSwapped)
        {
            // 100 is an arbitrary number to differentiate creatures
            currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility].id = currCreatureContext.lastTriggeredAbility + 100; // ID lastTriggered ability
            cooldownSystem.PutOnCooldown(currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility]); // Put last triggered ability on cooldown
        }
        // First creature
        else
        {
            // Debug.Log("New ID: " + currCreatureContext.lastTriggeredAbility);
            currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility].id = currCreatureContext.lastTriggeredAbility;
            cooldownSystem.PutOnCooldown(currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility]);

        }
    }

    public void PutBasicOnCD()
    {
        if (hasSwapped)
        {
            currCreatureContext.basicCreatureAttack.id = 10 + 100;
            cooldownSystem.PutOnCooldown(currCreatureContext.basicCreatureAttack);
        }
        else
        {
            currCreatureContext.basicCreatureAttack.id = 10;
            cooldownSystem.PutOnCooldown(currCreatureContext.basicCreatureAttack);

        }
    }

    // Checks if health reaches 0
    public void DeathCheck()
    {
        if(stats.getStat(ModiferType.CURR_HEALTH) <= 0)
        {
            SetStandbyState(true);
            PersistentData.Instance.PlayerDeath();
            //set standby mode, dont take damage
            
        }
       
    }

    public void HealMaxHealth()
    {
        stats.setStat(ModiferType.CURR_HEALTH, stats.getStat(ModiferType.MAX_HEALTH));
    }

    public void SetCombatState(bool _inCombat)
    {
        inCombat = _inCombat;

        // Assigns it to the currCreature
        if(currCreature != null)
        {
            currCreatureContext.inCombat = inCombat;
            // Assigns it to the swapCreature
            if(swapCreature != null)
            {
                swapCreature.GetComponent<CreatureAIContext>().inCombat = inCombat;
            }
        }
    }

    public void Slash()
    {
        if(inputs.usingMouse)
        {
            //Creates a movement vector for DoMovement to use on attacks. Moves player in direction of click
            Vector2 tempVec = new Vector2(directionTowardsMouse.x,directionTowardsMouse.z);
            tempVec.Normalize();
            attackMoveVec = new Vector3(tempVec.x, 0, tempVec.y);
            
            // Rotates player in direction of attack
            transform.rotation = Quaternion.LookRotation(new Vector3(directionTowardsMouse.x, 0, directionTowardsMouse.z));
            facingDirection = attackMoveVec; 
        }
    }
    
    // Hard set player location
    public void warpPlayer(Vector3 position)
    {
        agent.Warp(position);
    }


    public void SetStandbyState(bool state)
    {
       inStandby = state;
    }

    public void Pause()
    {
        playerInputs.SwitchCurrentActionMap("Menu");
    }

    public void Unpause()
    {
        playerInputs.SwitchCurrentActionMap("Player");
    }
}
