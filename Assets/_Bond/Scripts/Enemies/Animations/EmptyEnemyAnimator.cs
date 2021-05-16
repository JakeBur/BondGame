using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

public class EmptyEnemyAnimator : EnemyAnimator
{
    public float attackLength = 1.0f;
    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackLength);
        inAttack = false;
        boxCollider.enabled = false;
    }

    protected override void InternalAttack()
    {
        boxCollider.enabled = true;
        StartCoroutine("StartAttack");
    }

    protected override void InternalSpawn()
    {
        inSpawn = false;
    }

    protected override void InternalDeath()
    {
        inDeath = false;
    }
}
