//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

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
    public Dictionary<GameObject, InteractableBase> interactableObjects = new Dictionary<GameObject, InteractableBase>();
    [HideInInspector]
    public CharacterDialogManager characterDialogManager;

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

    [Header("Combat")]
    public bool inCombat;
    public bool isAttacking = false;
    public bool isHit;
    public Vector3 attackDestination;
    public Vector3 attackMoveVec;

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
        inputs.usingMouse = false;
    }

    // MOVEMENT FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////////

    public void doMovement(float movementModifier)
    {

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
            movementVector = lastMoveVec * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
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
    
    
    // F and North face button
    private void OnInteract()
    {
        // For dialog scenes
        if(inCharacterDialog)
        {
            characterDialogManager?.ContinueConvo();
        }
        // If interactable is close by
        else if(interactableObjects.Count > 0)
        {
            InteractableBase tempBase = null;
            GameObject tempObj = null;
            float closestDist = 20;

            // Find object that is the closest
            foreach(KeyValuePair<GameObject, InteractableBase> interactable in interactableObjects)
            {
                float distanceToObject = Vector3.Distance(interactable.Key.transform.position, gameObject.transform.position);
                if(distanceToObject < closestDist)
                {
                    closestDist = distanceToObject;
                    tempBase = interactable.Value;
                    tempObj = interactable.Key;
                }
            }

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
                PersistentData.Instance.UI.GetComponent<UIUpdates>().hideIntereactPrompt();
            }
        }
    }

    // Shift and 
    // Sets the autoAttack field of currCreature appropriately
    private void OnCreatureAutoAttack()
    {
        if(currCreatureContext != null)
        {
            if(currCreatureContext.autoAttack)
            {
                currCreatureContext.autoAttack = false;
            } 
            else 
            {
                currCreatureContext.autoAttack = true;
            }
        }
    }

    // Space and South button
    private void OnDash()
    {
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
        if(swapCreature != null)
        {
            var temp = currCreature;

            currCreature.GetComponent<CreatureAIContext>().agent.Warp(Vector3.zero);                // Warp to off map
            swapCreature.GetComponent<CreatureAIContext>().agent.Warp(backFollowPoint.position);    // Move new creature into position
            swapCreature.transform.position = backFollowPoint.position;

            currCreature = swapCreature;                                                            // Actual Swap
            currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
            currCreatureContext.isInPlayerRadius = false;
            currCreatureContext.isActive = true;

            swapCreature = temp;

            swapCreature.GetComponent<CreatureAIContext>().isActive = false;
            
            //Update the creature's enthusiasm bars
            swapCreature.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();
            currCreature.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();

            hasSwapped = !hasSwapped;
            
            PersistentData.Instance.UI.GetComponent<UIUpdates>().UpdateCreatureUI();                // UI Update

            SFXPlayer.PlayOneShot(SFX.CreatureSwapSFX, transform.position);                      // Sound
        }
        

    }


    //Slash (X)
    private void OnAttack1()
    {
        inputs.basicAttack = true;

        if(inputs.usingMouse)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(inputs.mousePos);
            int layerMask = 1 << 10;

            // If the raycast hits the ground
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // Debug.Log("RAYCAST : " + hit.transform.gameObject);
                // Debug.Log(hit.point);
                //gameObject.transform.LookAt(hit.point);

                attackDestination = hit.point;

                Vector3 direction = hit.point - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 9999f, 9999f);

                //Creates a movement vector for DoMovement to use on attacks. Moves player in direction of click
                Vector2 tempVec = new Vector2(newDirection.x,newDirection.z);
                tempVec.Normalize();
                attackMoveVec = new Vector3(tempVec.x, 0, tempVec.y);
                
                // Rotates player in direction of attack
                transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
                
            } 
        }
    }


    //Holding X
    private void OnHeavyAttack(InputValue value)
    {
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
        if( currCreature != null )
        {
            currCreatureContext.isAbilityTriggered = true;
            currCreatureContext.lastTriggeredAbility = 0;
        }

        PersistentData.Instance.UI.GetComponent<UIUpdates>().UsedAbility(1);
    }  


    //creature ability 2 (B)
    private void OnAttack3()
    {
        if( currCreature != null )
        {
            currCreatureContext.isAbilityTriggered = true;
            currCreatureContext.lastTriggeredAbility = 1;
        }

        PersistentData.Instance.UI.GetComponent<UIUpdates>().UsedAbility(2);
    }

    private void OnPause()
    {
        var canvas = PersistentData.Instance.PauseMenu.GetComponent<Canvas>();
        // Unpause
        if( canvas.enabled )
        {
            canvas.enabled = false;
            Time.timeScale = 1;
        }
        // Pause
        else 
        {   
            canvas.enabled = true;
            Time.timeScale = 0f;
        }

        SFXPlayer.PlayOneShot(SFX.MenuOpenSFX, transform.position);
        
    }

    private void OnTab()
    {
        var canvas = PersistentData.Instance.StatUI.GetComponent<Canvas>();
        canvas.enabled = !canvas.enabled;
        PersistentData.Instance.StatUI.GetComponent<StatUIFunctions>().UpdateCreatureStats(1);
       


        
        SFXPlayer.PlayOneShot(SFX.MenuOpenSFX, transform.position);

    }

    private void OnCrouch()
    {
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

    private void OnFruitSpawn()
    {
        var temp = Instantiate(fruit, transform.position, Quaternion.identity);
        temp.GetComponent<Fruit>().droppedByPlayer = true;
    }

    // Action Functions /////////////////////////////////////////////////////////////////////////////////////////////////

    public void befriendCreature()
    {
        // Temp fix when we can toggle this bool dynamically
        bool requirementMet = true;

        if(requirementMet)
        {
            // Already have 1 creature
            if(currCreature != null)
            {
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;                                  // Marks wild creature as not wild
                swapCreature = wildCreature;                                                                    // Stores wild creature as swap creature
                swapCreature.GetComponent<CreatureAIContext>().agent.Warp(Vector3.zero);                        // Warps off map
                swapCreature.GetComponent<CreatureAIContext>().isActive = false;

                ApplyCreatureRelics(swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager);  // Apply relics to the creature
            }
            else 
            { // first creature;
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                currCreature = wildCreature;
                currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
                currCreatureContext.isActive = true;
                ApplyCreatureRelics(currCreatureContext.creatureStats.statManager);
            }

            // HERMAN TODO: Place this in playerAnimator
            wildCreature.GetComponentInChildren<ParticleSystem>().Play();               //PLAYS HEARTS, NEED TO CHANGE SO IT WORKS WITH MULTIPLE P-SYSTEMS
            PersistentData.Instance.UI.GetComponent<UIUpdates>().UpdateCreatureUI();

            SetCombatState(inCombat);                                                   // Tells creature if it's in combat
        }

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
           // Hardcoded value: Teleports to Farm
           PersistentData.Instance.LoadScene(1);
           // Resets health to max
           stats.setStat(ModiferType.CURR_HEALTH, stats.getStat(ModiferType.MAX_HEALTH));

            //Reset creature if knocked out
            currCreatureContext.enthusiasmInteracted = false;
            currCreatureContext.creatureStats.statManager.setStat(ModiferType.CURR_ENTHUSIASM, currCreatureContext.creatureStats.statManager.getStat(ModiferType.MAX_ENTHUSIASM));
            //Update the creature's Enthusiasm Bar
            currCreatureContext.creatureTransform.gameObject.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();
       }
       
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
    
    // Hard set player location
    public void warpPlayer(Vector3 position)
    {
        agent.Warp(position);
    }
}
