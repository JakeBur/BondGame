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
    *   Constants
    *   Should be formatted "isX" or "inX" like a question
    *
    *   Public constants Can be read by other scripts
    *   But can only be set in here
    */

    public bool inAttack { get; private set; }
    public bool inHitstun { get; private set; }
    public bool inSpawn { get; private set; }
    public bool inDeath { get; private set; }

    private int attackStatesActive = 0;
    private float prevSpeed = 1;

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

    protected virtual void InternalEventPlayAttackSFX() {}

    public void EventColliderOn()
    {
        boxCollider.enabled = true;

        InternalEventColliderOn();
    }

    protected virtual void InternalEventColliderOn() {}

    public void EventColliderOff()
    {
        boxCollider.enabled = false;

        InternalEventColliderOff();
    }

    protected virtual void InternalEventColliderOff() {}

    public void EventDeathDone()
    {
        inDeath = false;

        InternalEventDeathDone();
    }

    protected virtual void InternalEventDeathDone() {}

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

    protected virtual void InternalSMBSpawnEnter() {}

    public void SMBSpawnExit()
    {
        inSpawn = false;

        InternalSMBSpawnExit();
    }

    protected virtual void InternalSMBSpawnExit() {}

    public void SMBAttackEnter()
    {
        attackStatesActive += 1;

        InternalSMBAttackEnter();
    }

    protected virtual void InternalSMBAttackEnter() {}

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

    protected virtual void InternalSMBAttackExit() {}

    /*
    *   Actual Functions
    *   Modifies the constants
    *   Called by the BT nodes
    */

    public void Pause()
    {
        prevSpeed = animator.speed;
        animator.speed = 0;

        InternalPause();
    }

    protected virtual void InternalPause() {}

    public void Play()
    {
        animator.speed = prevSpeed;

        InternalPlay();
    }

    protected virtual void InternalPlay() {}

    public void Spawn()
    {
        animator.SetTrigger( "Spawn" );
        SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);

        inSpawn = true;

        InternalSpawn();
    }

    protected virtual void InternalSpawn() {}

    public void Move( Vector3 moveVector ) 
    {
        Vector3 moveUnitVec = moveVector.normalized;
        animator.SetFloat( "MoveVelocity", moveVector.magnitude );
        animator.SetFloat( "MoveX", moveUnitVec[0] );
        animator.SetFloat( "MoveY", moveUnitVec[1] );

        InternalMove();
    }

    protected virtual void InternalMove() {}

    public void Attack()
    {
        animator.SetTrigger( "Attack1" );
        inAttack = true;

        InternalAttack();
    }

    protected virtual void InternalAttack() {}

    public void Hitstun()
    {
        animator.SetTrigger( "HurtStunTrigger" );
        animator.SetBool( "HurtStun", true );
        inHitstun = true;

        InternalHitstun();
    }

    protected virtual void InternalHitstun() {}

    public void HitstunDone()
    {
        animator.SetBool( "HurtStun", false );
        inHitstun = false;

        InternalHitstunDone();
    }

    protected virtual void InternalHitstunDone() {}

    public void Death()
    {
        animator.SetTrigger( "Death" );
        SFXPlayer.PlayOneShot(SFX.EnemyDeathSFX, transform.position);
        inDeath = true;

        InternalDeath();
    }

    protected virtual void InternalDeath() {}

}
