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

    /*
    *   FMOD Refs
    */
    private SFXManager SFX
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
    }

    public void EventColliderOn()
    {
        boxCollider.enabled = true;
    }

    public void EventColliderOff()
    {
        boxCollider.enabled = false;
    }

    public void EventDeathDone()
    {
        inDeath = false;
    }

    /*
    *   State Machine Behavior Triggers
    *   Triggered by State Machine Behaviors
    *   
    *   Functions should be prepended by SMB
    */

    public void SMBSpawnExit()
    {
        inSpawn = false;
    }

    public void SMBAttackEnter()
    {
        attackStatesActive += 1;
    }

    public void SMBAttackExit()
    {
        attackStatesActive -= 1;
        if( attackStatesActive < 1 )
        {
            inAttack = false;
            boxCollider.enabled = false;
        }
    }

    /*
    *   Actual Functions
    *   Modifies the constants
    *   Called by the BT nodes
    */

    public void Spawn()
    {
        animator.SetTrigger( "Spawn" );
        SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);

        inSpawn = true;
    }

    public void Move( Vector3 moveVector ) 
    {
        Vector3 moveUnitVec = moveVector.normalized;
        animator.SetFloat( "MoveVelocity", moveVector.magnitude );
        animator.SetFloat( "MoveX", moveUnitVec[0] );
        animator.SetFloat( "MoveY", moveUnitVec[1] );
    }

    public void Attack()
    {
        animator.SetTrigger( "Attack1" );
        inAttack = true;
    }

    public void ColliderOnOff()
    {
       boxCollider.enabled = !boxCollider.enabled;
    }

    public void Hitstun()
    {
        animator.SetTrigger( "HurtStunTrigger" );
        animator.SetBool( "HurtStun", true );
        inHitstun = true;
    }

    public void HitstunDone()
    {
        animator.SetBool( "HurtStun", false );
        inHitstun = false;
    }

    public void Death()
    {
        animator.SetTrigger( "Death" );
        SFXPlayer.PlayOneShot(SFX.EnemyDeathSFX, transform.position);
        inDeath = true;
    }

}
