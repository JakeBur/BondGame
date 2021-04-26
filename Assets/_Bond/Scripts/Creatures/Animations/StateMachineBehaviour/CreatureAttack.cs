using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAttack : CreatureSMB
{
    override public void OnStateEnter(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CreatureAnimator animator = GetCreatureAnimator( unityAnimator );

        animator.SMBAttackEnter();
    }

    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CreatureAnimator animator = GetCreatureAnimator( unityAnimator );

        animator.SMBAttackExit();
    }
}
