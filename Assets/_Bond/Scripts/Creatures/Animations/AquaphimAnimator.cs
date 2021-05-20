using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class AquaphimAnimator : CreatureAnimator
{
    protected override void InternalDefaultAttack()
    {
        inAbilityLockMovement = true;
    }

    public void Rain()
    {
        animator.SetTrigger("Rain");

        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void TurtleTop()
    {
        animator.SetTrigger("Turtle Top");
    }

    public void WaterShield()
    {
        animator.SetTrigger("Water Shield");

        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void WingGust()
    {
        animator.SetTrigger("Wing Gust");

        inAbilityLockMovement = true;
        inPreAbility = true;
    }
}
