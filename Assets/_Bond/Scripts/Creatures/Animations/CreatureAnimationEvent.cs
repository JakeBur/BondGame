using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationEvent : MonoBehaviour
{
    private CreatureAnimator creatureAnimator => transform.parent.GetComponent<CreatureAnimator>();

    public void PlayWalkSFX(int tag)
    {
        creatureAnimator.EventPlayWalkSFX(tag);
    }

    public void AbilityBegin()
    {
        creatureAnimator.EventAbilityBegin();
    }

    public void AbilityDone()
    {
        creatureAnimator.EventAbilityDone();
    }

    public void AttackDone()
    {
        creatureAnimator.EventAttackDone();
    }

    public void FreezeMovement()
    {
        creatureAnimator.EventFreezeMovement();
    }

    public void UnfreezeMovement()
    {
        creatureAnimator.EventUnfreezeMovement();
    }
}
