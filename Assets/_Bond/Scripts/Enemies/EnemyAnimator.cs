﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public GameObject model;
    private Animator animator => model.GetComponent<Animator>();
    

    public bool inAttack;
    public bool inHitstun;
    public bool inSpawn;

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

    public void Death()
    {
        FMODUnity.RuntimeManager.PlayOneShot(DeathSFX, transform.position);
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

    public void PlaySlamSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SlashSFX, transform.position);
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
