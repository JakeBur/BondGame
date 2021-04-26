using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class AquaphimAnimator : CreatureAnimator
{
    public void Rain()
    {
        animator.SetTrigger("Rain");
    }

    public void TurtleTop()
    {
        animator.SetTrigger("TurtleTop");
    }

    public void WaterShield()
    {
        animator.SetTrigger("WaterShield");
    }

    public void WingGust()
    {
        animator.SetTrigger("WingGust");
    }
}
