using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class SnailAnimator : CreatureAnimator
{
    protected override void InternalDefaultAttack()
    {
        inPreAbility = true;
    }

    public void EarthShaker()
    {
        animator.SetTrigger("EarthShaker");

        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void SlimeShot()
    {
        animator.SetTrigger("SlimeShot");

        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void SnailStand()
    {
        animator.SetTrigger("SnailStand");

        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void Wallop()
    {
        animator.SetTrigger("Wallop");

        inPreAbility = true;
    }

    public void EventPlayBasicAttackImpactSFX()
    {
        SFX.PlayEnemyPunchHitSFX(1, transform.position);
    }
}
