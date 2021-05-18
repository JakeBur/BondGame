// Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class EnemyAnimator : MonoBehaviour
{
    public GameObject model;
    private Animator animator => model.GetComponent<Animator>();

    public GameObject hitbox;
    public BoxCollider boxCollider => hitbox.GetComponent<BoxCollider>();

    /*
    *   Virtual Functions
    *   These functions are overriden in enemy-specific animator classes
    *   These functions are called in their respective base class function
    *   All Enemies will perform the code in base class functions
    */

    protected virtual void InternalEventPlayAttackSFX() {}
    protected virtual void InternalEventSpawnDone() {}
    protected virtual void InternalEventAttackDone() {}
    protected virtual void InternalEventColliderOn() {}
    protected virtual void InternalEventColliderOff() {}
    protected virtual void InternalEventDeathDone() {}

    protected virtual void InternalSMBSpawnEnter() {}
    protected virtual void InternalSMBSpawnExit() {}
    protected virtual void InternalSMBAttackEnter() {}
    protected virtual void InternalSMBAttackExit() {}

    protected virtual void InternalPause() {}
    protected virtual void InternalPlay() {}
    protected virtual void InternalSpawn() {}
    protected virtual void InternalMove() {}
    protected virtual void InternalAttack() {}
    protected virtual void InternalHitstun() {}
    protected virtual void InternalHitstunDone() {}
    protected virtual void InternalDeath() {}

    /*
    *   Constants
    *   Should be formatted "isX" or "inX" like a question
    *
    *   Public constants Can be read by other scripts
    *   But can only be set in here and child classes
    */

    public bool inAttack { get; protected set; }
    public bool inHitstun { get; protected set; }
    public bool inSpawn { get; protected set; }
    public bool inDeath { get; protected set; }

    protected int attackStatesActive = 0;
    protected float prevPlaybackSpeed = 1;

    /*
    *   FMOD Refs
    */
    protected SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    /*
    *   Animation Events
    *   Triggered in PlayerAnimationEvent.CS
    *
    *   Functions should be prepended by Event
    */

    public void EventPlayAttackSFX()
    {
        SFXPlayer.PlayOneShot(SFX.DonutSwipeSFX, transform.position);

        InternalEventPlayAttackSFX();
    }

    public void EventSpawnDone()
    {
        inSpawn = false;
        InternalEventSpawnDone();
    }

    public void EventAttackDone()
    {
        inAttack = false;
        boxCollider.enabled = false;

        InternalEventAttackDone();
    }

    public void EventColliderOn()
    {
        boxCollider.enabled = true;

        InternalEventColliderOn();
    }

    public void EventColliderOff()
    {
        boxCollider.enabled = false;

        InternalEventColliderOff();
    }

    public void EventDeathDone()
    {
        inDeath = false;

        InternalEventDeathDone();
    }

    /*
    *   State Machine Behavior Triggers
    *   Triggered by State Machine Behaviors
    *   
    *   Functions should be prepended by SMB
    */

    public void SMBSpawnEnter()
    {
        InternalSMBSpawnEnter();
    }

    public void SMBSpawnExit()
    {
        inSpawn = false;

        InternalSMBSpawnExit();
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
            boxCollider.enabled = false;

            InternalSMBAttackExit();
        }
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

    public void Spawn()
    {
        animator.SetTrigger( "Spawn" );

        inSpawn = true;

        InternalSpawn();
    }

    public void Move( Vector3 moveVector ) 
    {
        Vector3 moveUnitVec = moveVector.normalized;
        animator.SetFloat( "MoveVelocity", moveVector.magnitude );
        animator.SetFloat( "MoveX", moveUnitVec[0] );
        animator.SetFloat( "MoveY", moveUnitVec[1] );

        InternalMove();
    }

    public void Attack()
    {
        animator.SetTrigger( "Attack1" );
        inAttack = true;

        InternalAttack();
    }

    public void Hitstun()
    {
        animator.SetTrigger( "HurtStunTrigger" );
        animator.SetBool( "HurtStun", true );
        inHitstun = true;

        InternalHitstun();
    }

    public void HitstunDone()
    {
        animator.SetBool( "HurtStun", false );
        inHitstun = false;

        InternalHitstunDone();
    }

    public void Death()
    {
        animator.SetTrigger( "Death" );
        animator.SetBool("InDeath", true);
        SFXPlayer.PlayOneShot(SFX.EnemyDeathSFX, transform.position);
        inDeath = true;
        inAttack = false;
        inHitstun = false;
        inSpawn = false;

        InternalDeath();
    }

}
