using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class FragariaAnimator : CreatureAnimator
{
    public GameObject petalSawVFXPrefab;

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

    public void EventPlayPetalSawVFX()
    {
        GameObject slash = Instantiate(petalSawVFXPrefab, transform.position, Quaternion.identity);
        slash.transform.LookAt(transform.position + transform.forward);
    }
}
