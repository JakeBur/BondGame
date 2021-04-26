using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class DonutAnimator : EnemyAnimator
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
        SFX.Play3DWalkGrassSFX(tag, transform.position);
    }
}
