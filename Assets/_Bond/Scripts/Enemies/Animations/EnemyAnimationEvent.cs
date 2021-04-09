using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private EnemyAnimator enemyAnimator => transform.parent.GetComponent<EnemyAnimator>();

    public void ColliderOn()
    {
        enemyAnimator.EventColliderOn();
    }

    public void ColliderOff()
    {
        enemyAnimator.EventColliderOff();
    }

    public void PlayAttackSFX()
    {
        enemyAnimator.EventPlayAttackSFX();
    }

    public void DeathDone()
    {
        enemyAnimator.EventDeathDone();
    }
    
}
