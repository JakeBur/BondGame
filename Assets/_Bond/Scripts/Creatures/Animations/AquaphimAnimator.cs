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
        animator.SetTrigger("Turtle Top");
    }

    public void WaterShield()
    {
        animator.SetTrigger("Water Shield");
    }

    public void WingGust()
    {
        animator.SetTrigger("Wing Gust");
    }
}
