using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimationEvent : CreatureAnimationEvent
{
    protected RabbitAnimator rabbitAnimator => transform.parent.GetComponent<RabbitAnimator>();

    public void ShowKunai()
    {
        rabbitAnimator.EventShowKunai();
    }

    public void HideKunai()
    {
        rabbitAnimator.EventHideKunai();
    }

    public void ElusiveStepPoseDone()
    {
        rabbitAnimator.EventElusiveStepPoseDone();
    }
}
