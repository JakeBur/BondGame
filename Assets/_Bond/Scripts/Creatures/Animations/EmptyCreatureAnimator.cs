using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class EmptyCreatureAnimator : CreatureAnimator
{

    public void Ability()
    {
        inAbility = false;
    }

    protected override void InternalDefaultAttack()
    {
        inAttack = false;
    }

    protected override void InternalEat()
    {
        isEating = false;
    }

    protected override void InternalInteractPOI()
    {
        isInteractPOI = false;
    }

    protected override void InternalPlayerNoticed()
    {
        isPlayerNoticed = false;
    }
}
