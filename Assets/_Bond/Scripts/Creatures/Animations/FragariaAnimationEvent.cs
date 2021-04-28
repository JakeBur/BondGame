using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragariaAnimationEvent : CreatureAnimationEvent
{
    protected FragariaAnimator fragariaAnimator => transform.parent.GetComponent<FragariaAnimator>();

    public void PlayPetalSawVFX() => fragariaAnimator.EventPlayPetalSawVFX();
}
