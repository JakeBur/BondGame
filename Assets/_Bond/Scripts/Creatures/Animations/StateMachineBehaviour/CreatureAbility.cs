using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAbility : CreatureSMB
{
    override public void OnStateEnter(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CreatureAnimator animator = GetCreatureAnimator( unityAnimator );

        animator.SMBAbilityEnter();
    }

    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CreatureAnimator animator = GetCreatureAnimator( unityAnimator );

        animator.SMBAbilityExit();
    }
}
