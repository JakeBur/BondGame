using System.Collections;
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
        
    }

    protected override void InternalSMBSpawnEnter()
    {
        
    }

    protected override void InternalSMBSpawnExit()
    {
        
    }

    protected override void InternalSMBAttackEnter()
    {
        
    }

    protected override void InternalSMBAttackExit()
    {
        
    }

    protected override void InternalSpawn()
    {
        inSpawn = false;
    }

    protected override void InternalDeath()
    {
        inDeath = false;
    }
}
