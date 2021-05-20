using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimator : CreatureAnimator
{
    public GameObject kunaiModel;
    private SkinnedMeshRenderer kunaiMesh => kunaiModel.GetComponent<SkinnedMeshRenderer>();

    public bool inElusiveStepPose { get; protected set; }

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
    }

    public void ElusiveStepPose()
    {
        inElusiveStepPose = true;

        animator.SetTrigger("ElusiveStepPose");

        EventShowKunai();
    }

    public void ElusiveStepStrike()
    {
        animator.SetTrigger("ElusiveStepStrike");

        EventShowKunai();
    }

    public void Quickdraw()
    {
        animator.SetTrigger("Quickdraw");

        EventShowKunai();
    }

    public void Shotgun()
    {
        animator.SetTrigger("Shotgun");

        EventShowKunai();
    }
}
