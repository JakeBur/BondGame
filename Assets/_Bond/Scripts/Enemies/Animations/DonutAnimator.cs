using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class DonutAnimator : EnemyAnimator
{
    public GameObject armModel;
    private SkinnedMeshRenderer armMesh => armModel.GetComponent<SkinnedMeshRenderer>();

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

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
        SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);
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
        SFXPlayer.PlayOneShot(SFX.DonutSpawnSFX, transform.position);
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
}
