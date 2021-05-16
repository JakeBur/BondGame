using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyEnemyAnimationEvent : EnemyAnimationEvent
{
    protected EmptyEnemyAnimator emptyEnemyAnimator => transform.parent.GetComponent<EmptyEnemyAnimator>();

}
