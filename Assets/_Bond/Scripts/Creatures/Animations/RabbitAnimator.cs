using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimator : CreatureAnimator
{
    public GameObject kunaiModel;
    private SkinnedMeshRenderer kunaiMesh => kunaiModel.GetComponent<SkinnedMeshRenderer>();

    public bool inElusiveStepPose { get; protected set; }

    protected override void InternalDefaultAttack()
    {
        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void EventHideKunai()
    {
        kunaiMesh.enabled = false;
    }

    public void EventShowKunai()
    {
        kunaiMesh.enabled = true;
    }

    public void EventElusiveStepPoseDone()
    {
        inElusiveStepPose = false;
    }

    public void BicycleKick()
    {
        animator.SetTrigger("BicycleKick");

        EventHideKunai();

        inAbilityLockMovement = true;
    }

    public void ElusiveStepPose()
    {
        animator.SetTrigger("ElusiveStepPose");
        animator.ResetTrigger("ElusiveStepStrike");

        EventShowKunai();

        inAbilityLockMovement = true;
        inElusiveStepPose = true;
    }

    public void ElusiveStepStrike()
    {
        animator.SetTrigger("ElusiveStepStrike");
        animator.ResetTrigger("ElusiveStepPose");

        EventShowKunai();

        inAbilityLockMovement = true;
    }

    public void Quickdraw()
    {
        animator.SetTrigger("Quickdraw");

        EventShowKunai();

        inAbilityLockMovement = true;
        inPreAbility = true;
    }

    public void Shotgun()
    {
        animator.SetTrigger("Shotgun");

        EventShowKunai();

        inAbilityLockMovement = true;
    }
}
