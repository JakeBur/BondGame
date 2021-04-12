using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : EnemySMB
{
    override public void OnStateEnter(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAnimator animator = GetEnemyAnimator( unityAnimator );

        animator.SMBSpawnEnter();
    }

    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAnimator animator = GetEnemyAnimator( unityAnimator );

        animator.SMBSpawnExit();
    }
}
