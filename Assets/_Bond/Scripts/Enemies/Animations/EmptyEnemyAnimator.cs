using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;

// NEED TO FIGURE OUT WHAT TO DO WITH HITBOXES
public class EmptyEnemyAnimator : EnemyAnimator
{
    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1);
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
