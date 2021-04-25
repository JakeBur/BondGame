﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class PufferfishAnimator : EnemyAnimator
{
    public GameObject armModel;
    private SkinnedMeshRenderer armMesh => armModel.GetComponent<SkinnedMeshRenderer>();

    private bool alreadySpawning = false;

    private void Awake()
    {
        ArmOff();
    }

    public void ArmOff()
    {
        armMesh.enabled = false;
    }

    protected override void InternalSMBSpawnEnter()
    {
        //SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);
        armMesh.enabled = true;
    }

    protected override void InternalSMBSpawnExit()
    {
        armMesh.enabled = false;
    }

    protected override void InternalSMBAttackEnter()
    {
        armMesh.enabled = true;
    }

    protected override void InternalSMBAttackExit()
    {
        armMesh.enabled = false;
    }

    protected override void InternalSpawn()
    {
        //SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);
        inSpawn = false;
    }

    protected override void InternalDeath()
    {
        inDeath = false;
    }

    public void EventPlayDonutSpawnExtendSFX()
    {
        //---------------------------------------------
        // check if there is one other spawning event
        // occuring to prevent multiple calls
        //---------------------------------------------
        if (!SFX.IsAlreadySpawning())
        {
            SFXPlayer.PlayOneShot(SFX.DonutSpawnExtendSFX, transform.position);
            SFX.SetSpawning(true);
            alreadySpawning = false;
        }
        else
        {
            alreadySpawning = true;
        }
    }

    public void EventPlayDonutSpawnGrabSFX()
    {
        //--------------------------
        // ditto for all spawn SFX
        //--------------------------
        if (!alreadySpawning)
        {
            SFXPlayer.PlayOneShot(SFX.DonutSpawnGrabSFX, transform.position);
        }
    }

    public void EventPlayDonutSpawnDragSFX()
    {
        if (!alreadySpawning)
        {
            SFXPlayer.PlayOneShot(SFX.DonutSpawnDragSFX, transform.position);
        }
    }

    public void EventPlayDonutAttackExtendSFX()
    {
        SFXPlayer.PlayOneShot(SFX.DonutAttackExtendSFX, transform.position);
    }

    public void EventPlayDonutRetractSFX(int call)
    {
        if (call == 1)
        {
            SFXPlayer.PlayOneShot(SFX.DonutRetractSFX + " 2", transform.position);
            return;
        }
        SFXPlayer.PlayOneShot(SFX.DonutRetractSFX, transform.position);
    }

    public void EventPlayDonutWalkSFX(int tag)
    {
        SFX.Play3DWalkGrassSFX(tag, transform.position);
    }
}