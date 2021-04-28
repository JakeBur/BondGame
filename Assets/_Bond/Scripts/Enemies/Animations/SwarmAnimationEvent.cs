using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmAnimationEvent : EnemyAnimationEvent
{
    protected SwarmAnimator swarmAnimator => transform.parent.GetComponent<SwarmAnimator>();

    public void ArmOff()
    {
        
    }
    
    public void PlayDonutSpawnExtendSFX()
    {
        swarmAnimator.EventPlayDonutSpawnExtendSFX();
    }

    public void PlayDonutSpawnGrabSFX()
    {
        swarmAnimator.EventPlayDonutSpawnGrabSFX();
    }

    public void PlayDonutSpawnDragSFX()
    {
        swarmAnimator.EventPlayDonutSpawnDragSFX();
    }

    public void PlayDonutAttackExtendSFX()
    {
        swarmAnimator.EventPlayDonutAttackExtendSFX();
    }

    public void PlayDonutRetractSFX(int call)
    {
        swarmAnimator.EventPlayDonutRetractSFX(call);
    }

    public void PlayDonutWalkSFX()
    {
        swarmAnimator.EventPlayDonutWalkSFX(2);
    }
}
