using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySMB : StateMachineBehaviour
{
    public EnemyAnimator GetEnemyAnimator(Animator animator)
    {
        return animator.gameObject.transform.parent.GetComponent<EnemyAnimator>();
    }
}
