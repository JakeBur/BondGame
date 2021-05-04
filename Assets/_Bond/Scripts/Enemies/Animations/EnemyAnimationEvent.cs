using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    protected virtual EnemyAnimator enemyAnimator => transform.parent.GetComponent<EnemyAnimator>();

    public void SpawnDone()
    {
        enemyAnimator.EventSpawnDone();
    }

    public void AttackDone()
    {
        enemyAnimator.EventAttackDone();
    }

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
