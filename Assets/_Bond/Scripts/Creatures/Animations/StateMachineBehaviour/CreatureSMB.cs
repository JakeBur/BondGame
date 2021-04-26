using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSMB : StateMachineBehaviour
{
    public CreatureAnimator GetCreatureAnimator(Animator animator)
    {
        return animator.gameObject.transform.parent.GetComponent<CreatureAnimator>();
    }
}
