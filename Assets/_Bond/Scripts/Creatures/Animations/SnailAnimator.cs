using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class SnailAnimator : CreatureAnimator
{

    public void EarthShaker()
    {
        animator.SetTrigger("EarthShaker");
    }

    public void SlimeShot()
    {
        animator.SetTrigger("SlimeShot");
    }

    public void SnailStand()
    {
        animator.SetTrigger("SnailStand");
    }

    public void Wallop()
    {
        animator.SetTrigger("Wallop");
    }
}
