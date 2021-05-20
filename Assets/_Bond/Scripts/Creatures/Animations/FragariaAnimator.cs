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

    public void EventPlayPetalSawSFX()
    {
        SFXPlayer.PlayOneShot(SFX.FragariaPetalSawSFX, transform.position);
    }

    public void EventPlayPetalSawVFX()
    {
        GameObject slash = Instantiate(petalSawVFXPrefab, transform.position, Quaternion.identity);
        slash.transform.LookAt(transform.position + transform.forward);
	}

    public void PetalThrow()
    {
        animator.SetTrigger("PetalThrow");
    }

    public void EventPlayPetalThrowWhooshSFX(int count)
    {
        SFX.PlayFragariaPetalThrowWhooshSFX(count, transform.position);
    }

    public void EventPlayPetalThrowEndingSFX()
    {
        SFXPlayer.PlayOneShot(SFX.FragariaPetalThrowEndingSFX, transform.position);
    }

    public void SporeToss()
    {
        animator.SetTrigger("SporeToss");
    }

    public void SunBeam()
    {
        animator.SetTrigger("SunBeam");
    }
	
    public void EventPlayBasicAttackImpactSFX()
    {
        SFX.PlayEnemyPunchHitSFX(0, transform.position);
    }
}
