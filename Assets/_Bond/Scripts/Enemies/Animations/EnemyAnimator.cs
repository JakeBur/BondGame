// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public GameObject model;
    private Animator animator => model.GetComponent<Animator>();

    /*
    *   Constants
    *   Should be formatted "isX" or "inX" like a question
    *
    *   Public constants Can be read by other scripts
    *   But can only be set in here
    *   
    */

    public bool inAttack { get; private set; }
    public bool inHitstun { get; private set; }
    public bool inSpawn { get; private set; }

    private int attackStatesActive = 0;

    /*
    *   FMOD Refs
    *
    */

    [FMODUnity.EventRef]
    public string SlashSFX;

    [FMODUnity.EventRef]
    public string SpawnSFX;

    [FMODUnity.EventRef]
    public string DeathSFX;

    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SpawnSFX, transform.position);
    }

    /*
    *   Animation Events
    *   Triggered in PlayerAnimationEvent.CS
    *
    *   Functions should be prepended by Event
    */

    public void EventPlayAttackSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SlashSFX, transform.position);
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
        inSpawn = true;
    }

    public void Move(Vector3 moveSpeed) 
    {
        Vector3 moveUnitVec = moveSpeed.Normalize();
        animator.SetFloat( "MoveVelocity", moveSpeed.magnitude );
        animator.SetFloat( "MoveX", moveUnitVec[0] );
        animator.SetFloat( "MoveY", moveUnitVec[1] );
    }

    public void Attack()
    {
        animator.SetTrigger( "Attack1" );
        inAttack = true;
    }

    public void Hitstun()
    {
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
        FMODUnity.RuntimeManager.PlayOneShot(DeathSFX, transform.position);
    }

}
