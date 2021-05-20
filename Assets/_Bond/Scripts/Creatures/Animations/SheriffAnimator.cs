using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffAnimator : CreatureAnimator
{
    public void BicycleKick()
    {
        animator.SetTrigger("BicycleKick");
    }

    public void ElusiveStepPose()
    {
        animator.SetTrigger("ElusiveStepPose");
    }

    public void ElusiveStepStrike()
    {
        animator.SetTrigger("ElusiveStepStrike");
    }

    public void Quickdraw()
    {
        animator.SetTrigger("Quickdraw");
    }

    public void Shotgun()
    {
        animator.SetTrigger("Shotgun");
    }
}
