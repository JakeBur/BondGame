using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



/*
*   Written by Herman
*/

public class PlayerAnimator : MonoBehaviour
{
    [Serializable]
    struct VFXData
    {
        public List<GameObject> slashes;
    }

    public GameObject model;
    private Animator animator => model.GetComponent<Animator>();
    private PlayerController playerController => GetComponent<PlayerController>();

    [Header("VFX")]
    public ParticleSystem heavyChargeVfx;
    public ParticleSystem heavyHitVfx;
    public ParticleSystem slashVfx;
    [SerializeField]
    private VFXData vfxData;

    /*
    *   Constants
    *   Should be formatted "isX" like a question
    *
    *   Public constants Can be read by other scripts
    *   But can only be set in here
    *   
    */
    public bool isAttack { get; private set; }
    public bool isAttackFollowThrough { get; private set; }
    public bool isDash { get; private set; }
    public bool isHeavyAttack { get; private set; }
    public bool isHurt { get; private set; }
    public bool isRun { get; private set; }

    [FMODUnity.EventRef]
    public string SwordSwingSFX;
    [FMODUnity.EventRef]
    public string WalkSFX;
    [FMODUnity.EventRef]
    public string RollInitialSFX;
    [FMODUnity.EventRef]
    public string RollSecondarySFX;

    private int attackStatesActive = 0;
    private float moveMagnitude = 0f;

    void Update()
    {
        if ( isRun && moveMagnitude > 0 )
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    /*
    *   Animation Events
    *   Triggered in PlayerAnimationEvent.CS
    */

    public void EventAttackDone()
    {
        isAttack = false;
    }

    public void EventHeavyAttackDone()
    {
        isHeavyAttack = false;
    }

    /*
    *   State Machine Behavior Triggers
    *   Triggered by State Machine Behaviors
    */

    public void SMBAttackEnter()
    {
        attackStatesActive += 1;
    }

    public void SMBAttackExit()
    {
        attackStatesActive -= 1;
        if( attackStatesActive < 1 )
        {
            isAttack = false;
            isAttackFollowThrough = false;
        }
    }

    public void SMBDashExit()
    {
        isDash = false;
        animator.ResetTrigger("Dash");
    }

    public void SMBHeavyAttackExit()
    {
        isHeavyAttack = false;
    }

    public void SMBHurtExit()
    {
        isHurt = false;
        animator.ResetTrigger("isHit");
    }

    /*
    *   Actual Functions
    *   Modifies the constants
    *   Called by the states
    */

    public void Attack( int num )
    {
        isAttack = true;
        isAttackFollowThrough = true;

        animator.SetTrigger("Attack" + num.ToString() );
    }

    public void ResetAttackAnim()
    {
        isAttack = false;
        isAttackFollowThrough = false;
    }

    public void ResetAllAttackAnims()
    {
        isAttack = false;
        isAttackFollowThrough = false;

        animator.ResetTrigger("Attack0");
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack4");

        animator.ResetTrigger("HeavyAttack");
        animator.ResetTrigger("HeavyCharge");
    }

    public void HeavyCharge(bool state)
    {
        if( state )
        {
            animator.SetTrigger("HeavyCharge");

            heavyChargeVfx.Play();
        }
        else
        {
            heavyChargeVfx.Stop();
        }
    }

    public void HeavyAttack()
    {
        isHeavyAttack = true;

        animator.SetTrigger("HeavyAttack");
        animator.ResetTrigger("HeavyCharge");

        heavyHitVfx.Play();
    }

    public void Crouch(bool state)
    {
        // Herman TODO: Make this value lerp
        if( state )
        {
            animator.SetFloat("Standing_Crouch_Blend", 1f );
        }
        else
        {
            animator.SetFloat("Standing_Crouch_Blend", 0f );
        }
    }

    public void Dash( float constant )
    {
        isDash = true;
        animator.SetTrigger("Dash");
        animator.SetFloat("DashConstant", 1/constant);

        this.ResetAllAttackAnims();
    }

    public void Hurt()
    {
        isHurt = true;

        animator.SetTrigger("isHit");
    }

    public void Idle(bool state)
    {
        if( state )
        {
            this.ResetAllAttackAnims();

            animator.ResetTrigger("Dash");
            animator.ResetTrigger("isHit");

            isRun = true;
        }
        else
        {
            isRun = false;

            animator.SetBool("Run", false);
        }
        
    }

    public void Move(Vector3 movementVector)
    {
        moveMagnitude = movementVector.magnitude;
    }

    // VISUAL FX

    public void PlaySlashVFX(int animationIndex)
    {
        //slashVfx.Play();
        if(vfxData.slashes.Count <= animationIndex)
        {
            Debug.LogError($"Slash VFX prefab #{animationIndex} does not exist");
            return;
        }
        GameObject slash = Instantiate(vfxData.slashes[animationIndex], transform.position, Quaternion.identity);
        //slash.transform.LookAt(transform.forward);
        slash.transform.LookAt(transform.position + transform.forward);
    }

    // SOUND FX

    public void PlaySlashSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SwordSwingSFX, transform.position);
    }

    public void PlayWalkSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(WalkSFX, transform.position);
    }

    public void PlayRollInitialSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(RollInitialSFX, transform.position);
    }

    public void PlayRollSecondarySFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(RollSecondarySFX, transform.position);
    }



    public void EnableHitbox()//Jamo making these so we can control how long hitboxes stay out in each attack
    {
        if(playerController.fsm.currentState == playerController.fsm.Slash0)
        {
            playerController.hitBoxes.slash0.SetActive(true);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash1)
        {
            playerController.hitBoxes.slash1.SetActive(true);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash2)
        {
            playerController.hitBoxes.slash1.SetActive(true);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash3)
        {
            playerController.hitBoxes.slash1.SetActive(true);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash4)
        {
            playerController.hitBoxes.slash1.SetActive(true);
        }
    
    }


    public void DisableHitbox()//Jamo making these so we can control how long hitboxes stay out in each attack
    {
        if(playerController.fsm.currentState == playerController.fsm.Slash0)
        {
            playerController.hitBoxes.slash0.SetActive(false);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash1)
        {
            playerController.hitBoxes.slash1.SetActive(false);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash2)
        {
            playerController.hitBoxes.slash1.SetActive(false);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash3)
        {
            playerController.hitBoxes.slash1.SetActive(false);
        }
        else if(playerController.fsm.currentState == playerController.fsm.Slash4)
        {
            playerController.hitBoxes.slash1.SetActive(false);
        }
    }
}
