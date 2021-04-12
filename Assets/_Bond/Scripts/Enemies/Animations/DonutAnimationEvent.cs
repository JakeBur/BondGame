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
    
}
