using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAnimationEvent : CreatureAnimationEvent
{
    protected SnailAnimator snailAnimator => transform.parent.GetComponent<SnailAnimator>();

    public void PlayBasicAttackImpactSFX()
    {
        snailAnimator.EventPlayBasicAttackImpactSFX();
    }
}
