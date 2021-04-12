using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class DonutAnimator : EnemyAnimator
{
    public GameObject armModel;
    private SkinnedMeshRenderer armMesh => armModel.GetComponent<SkinnedMeshRenderer>();

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
}
