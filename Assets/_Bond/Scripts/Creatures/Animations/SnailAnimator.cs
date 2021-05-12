using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class SnailAnimator : CreatureAnimator
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

    public void EventPlayPetalSawVFX()
    {
        GameObject slash = Instantiate(petalSawVFXPrefab, transform.position, Quaternion.identity);
        slash.transform.LookAt(transform.position + transform.forward);
        Debug.Log("playing from animation event");
	}
	
    public void EventPlayBasicAttackImpactSFX()
    {
        SFXPlayer.PlayOneShot(SFX.EnemyPunchHitSFX);
    }
}
