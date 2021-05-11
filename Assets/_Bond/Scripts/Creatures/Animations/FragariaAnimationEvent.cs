using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragariaAnimationEvent : CreatureAnimationEvent
{
    protected FragariaAnimator fragariaAnimator => transform.parent.GetComponent<FragariaAnimator>();

    public void PlayPetalSawVFX() => fragariaAnimator.EventPlayPetalSawVFX();

    public void PlayPetalSawSFX()
    {
        fragariaAnimator.EventPlayPetalSawSFX();
    }

    public void PlayPetalThrowWhooshSFX(int count)
    {
        fragariaAnimator.EventPlayPetalThrowWhooshSFX(count);
    }

    public void PlayPetalThrowEndingSFX()
    {
        fragariaAnimator.EventPlayPetalThrowEndingSFX();
    }

    public void PlayBasicAttackImpactSFX()
    {
        fragariaAnimator.EventPlayBasicAttackImpactSFX();
    }
}
