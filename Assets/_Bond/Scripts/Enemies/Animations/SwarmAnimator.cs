using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class SwarmAnimator : EnemyAnimator
{

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
        SFX.Play3DWalkSFX(tag, transform);
    }

    protected override void InternalSMBAttackEnter()
    {
        boxCollider.enabled = true;
    }

    protected override void InternalSMBAttackExit()
    {
        boxCollider.enabled = false;
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
