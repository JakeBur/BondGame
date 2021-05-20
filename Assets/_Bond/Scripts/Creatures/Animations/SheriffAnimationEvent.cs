using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffAnimationEvent : CreatureAnimationEvent
{
    protected SheriffAnimator sheriffAnimator => transform.parent.GetComponent<SheriffAnimator>();
}
