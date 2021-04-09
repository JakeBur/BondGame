using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemySMB
{
    override public void OnStateEnter(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAnimator animator = GetEnemyAnimator( unityAnimator );

        animator.SMBAttackEnter();
    }

    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAnimator animator = GetEnemyAnimator( unityAnimator );

        animator.SMBAttackExit();
    }
}
