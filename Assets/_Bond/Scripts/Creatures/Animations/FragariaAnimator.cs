using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class FragariaAnimator : CreatureAnimator
{
    public void PetalSaw()
    {
        animator.SetTrigger("PetalSaw");
    }

    public void PetalThrow()
    {
        animator.SetTrigger("PetalThrow");
    }

    public void SporeToss()
    {
        animator.SetTrigger("SporeToss");
    }

    public void SunBeam()
    {
        animator.SetTrigger("SunBeam");
    }
}
