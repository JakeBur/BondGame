using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class DonutAnimator : EnemyAnimator
{
    public GameObject slashVFXPrefab;

    public GameObject armModel;

    //----------------------------------------------
    // Raycast origin for determining footstep SFX
    //----------------------------------------------
    public GameObject raycastOrigin;
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
        armMesh.enabled = true;
    }

    protected override void InternalSMBSpawnExit()
    {
        if( attackStatesActive == 0 )
        {
            armMesh.enabled = false;
        }
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
        
    }

    public void EventPlayDonutSpawnExtendSFX()
    {
        SFXPlayer.PlayOneShot(SFX.DonutSpawnExtendSFX, transform.position);
    }

    public void EventPlayDonutSpawnGrabSFX()
    {
        SFXPlayer.PlayOneShot(SFX.DonutSpawnGrabSFX, transform.position);
    }

    public void EventPlayDonutSpawnDragSFX()
    {
        SFXPlayer.PlayOneShot(SFX.DonutSpawnDragSFX, transform.position);
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
        //SFX.Play3DWalkGrassSFX(tag, transform.position);
        SFX.Play3DWalkSFX(tag, raycastOrigin.transform);
    }

    public void EventPlaySlashVFX()
    {
        Instantiate(slashVFXPrefab, transform);
    }
}
