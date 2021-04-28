// Herman, Enrico

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class CreatureAnimator : MonoBehaviour
{
    public GameObject model;
    public Animator animator => model.GetComponent<Animator>();

    /*
    *   Virtual Functions
    *   These functions are overriden in enemy-specific animator classes
    *   These functions are called in their respective base class function
    *   All Enemies will perform the code in base class functions
    */

    protected virtual void InternalEventPlayWalkSFX() {}

    protected virtual void InternalSMBAbilityEnter() {}
    protected virtual void InternalSMBAbilityExit() {}
    protected virtual void InternalSMBAttackEnter() {}
    protected virtual void InternalSMBAttackExit() {}
    protected virtual void InternalSMBInteractPOIExit() {}
    protected virtual void InternalSMBPlayerNoticedExit() {}

    protected virtual void InternalPause() {}
    protected virtual void InternalPlay() {}
    protected virtual void InternalCry() {}
    protected virtual void InternalDefaultAttack() {}
    protected virtual void InternalEat() {}
    protected virtual void InternalInteractFlower() {}
    protected virtual void InternalInteractTree() {}
    protected virtual void InternalInteractPOI() {}
    protected virtual void InternalMove() {}
    protected virtual void InternalPlayerNoticed() {}
    protected virtual void InternalRelax() {}

    /*
    *   Constants
    *   Should be formatted "isX" or "inX" like a question
    *
    *   Public constants Can be read by other scripts
    *   But can only be set in here and child classes
    */

    public bool inAbility { get; protected set; }
    public bool inAttack { get; protected set; }
    public bool isEating { get; protected set; }
    public bool isInteractPOI { get; protected set; }
    public bool isPlayerNoticed { get; protected set; }

    protected int attackStatesActive = 0;
    protected int abilityStatesActive = 0;
    protected float prevPlaybackSpeed = 1;

    /*
    *   FMOD Refs
    */

    protected private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    /*
    *   Animation Events
    *   Triggered in PlayerAnimationEvent.CS
    *
    *   Functions should be prepended by Event
    */

    public void EventPlayWalkSFX(int tag)
    {
        SFX.Play3DWalkGrassSFX(tag, transform.position);

        InternalEventPlayWalkSFX();
    }

    /*
    *   State Machine Behavior Triggers
    *   Triggered by State Machine Behaviors
    *   
    *   Functions should be prepended by SMB
    */

    public void SMBAbilityEnter()
    {
        abilityStatesActive += 1;

        InternalSMBAbilityEnter();
    }

    public void SMBAbilityExit()
    {
        abilityStatesActive -= 1;
        if( abilityStatesActive < 1 )
        {
            inAbility = false;

            InternalSMBAbilityExit();
        }
    }

    public void SMBAttackEnter()
    {
        attackStatesActive += 1;

        InternalSMBAttackEnter();
    }

    public void SMBAttackExit()
    {
        attackStatesActive -= 1;
        if( attackStatesActive < 1 )
        {
            inAttack = false;

            InternalSMBAttackExit();
        }
    }

    public void SMBInteractPOIExit()
    {
        isInteractPOI = false;

        InternalSMBInteractPOIExit();
    }

    public void SMBPlayerNoticedExit()
    {
        isPlayerNoticed = false;

        InternalSMBPlayerNoticedExit();
    }

    /*
    *   Actual Functions
    *   Modifies the constants
    *   Called by the BT nodes
    */

    public void Pause()
    {
        prevPlaybackSpeed = animator.speed;
        animator.speed = 0;

        InternalPause();
    }

    public void Play()
    {
        if( prevPlaybackSpeed != 0 )
        {
            animator.speed = prevPlaybackSpeed;
        }
        else
        {
            animator.speed = 1;
        }

        InternalPlay();
    }

    public void Cry()
    {
        animator.SetTrigger( "Cry" );
        InternalCry();
    }

    public void DefaultAttack()
    {
        animator.SetTrigger( "DefaultAttack" );
        inAttack = true;

        InternalDefaultAttack();
    }

    public void Eat()
    {
        animator.SetTrigger( "Eat" );
        isEating = true;

        InternalEat();
    }

    public void Move(Vector3 moveSpeed) 
    {
        if(moveSpeed.magnitude > 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        InternalMove();
    }

    public void InteractFlower()
    {
        animator.SetTrigger("InteractFlower");
        isInteractPOI = true;

        InternalInteractFlower();
    }

    public void InteractTree()
    {
        Relax();
        isInteractPOI = true;

        InternalInteractTree();
    }

    public void PlayerNoticed()
    {
        animator.SetTrigger("PlayerNoticed");
        isPlayerNoticed = true;

        InternalPlayerNoticed();
    }

    public void Relax()
    {
        animator.SetTrigger("Relax");

        InternalRelax();
    }

    public void InteractPOI(string _tag)
    {
        switch(_tag)
        {
            case "POITree" :
                isInteractPOI = true;
                InteractTree();
                break;
            case "POIFlower" :
                isInteractPOI = true;
                InteractFlower();
                break;
            case "POIOther" :
                isInteractPOI = true;
                InteractFlower();
                break;
            default :
                isInteractPOI = true;
                InteractFlower();
                break;
        }

        InternalInteractPOI();
    }

    // Remove since SMB function should handle it
    public void interactPOIFalse()
    {
        isInteractPOI = false;
    }
    
}
