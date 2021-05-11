using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutAnimationEvent : EnemyAnimationEvent
{
    protected DonutAnimator donutAnimator => transform.parent.GetComponent<DonutAnimator>();

    public void ArmOff()
    {
        donutAnimator.ArmOff();
    }

    public void PlaySlashVFX()
    {
        donutAnimator.EventPlaySlashVFX();
    }
    
    public void PlayDonutSpawnExtendSFX()
    {
        donutAnimator.EventPlayDonutSpawnExtendSFX();
    }

    public void PlayDonutSpawnGrabSFX()
    {
        donutAnimator.EventPlayDonutSpawnGrabSFX();
    }

    public void PlayDonutSpawnDragSFX()
    {
        donutAnimator.EventPlayDonutSpawnDragSFX();
    }

    public void PlayDonutAttackExtendSFX()
    {
        donutAnimator.EventPlayDonutAttackExtendSFX();
    }

    public void PlayDonutRetractSFX(int call)
    {
        donutAnimator.EventPlayDonutRetractSFX(call);
    }

    public void PlayDonutWalkSFX()
    {
        donutAnimator.EventPlayDonutWalkSFX(2);
    }
}
