// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class EnemyAnimator : MonoBehaviour
{
    public GameObject model;
    private Animator animator => model.GetComponent<Animator>();
    

    public bool inAttack;
    public bool inHitstun;
    public bool inSpawn;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    private void Awake()
    {
        SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);
    }

    public void Death()
    {
        SFXPlayer.PlayOneShot(SFX.EnemyDeathSFX, transform.position);
    }

    public void Move(Vector3 moveSpeed) 
    {
        if(moveSpeed.magnitude > 0)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
        inAttack = true;
    }

    public void AttackDone()
    {
        inAttack = false;
    }

    public void Hitstun()
    {
        animator.SetTrigger("isHit");
        inAttack = false; //So we don't get stuck in attack
        inHitstun = true;
    }

    public void HitstunDone()
    {
        inHitstun = false;
    }

    public void PlaySwipeSFX()
    {
        SFXPlayer.PlayOneShot(SFX.DonutSwipeSFX, transform.position);
    }

    public void Spawn()
    {
        //animator.SetTrigger("spawn");
        inSpawn = true;
    }
    
    public void SpawnDone()
    {
        inSpawn = false;
    }

}
