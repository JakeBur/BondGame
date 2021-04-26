using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaphimAnimationEvent : CreatureAnimationEvent
{
    protected AquaphimAnimator aquaphimAnimator => transform.parent.GetComponent<AquaphimAnimator>();
}
