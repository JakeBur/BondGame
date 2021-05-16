using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCreatureAnimationEvent : CreatureAnimationEvent
{
    protected EmptyCreatureAnimator emptyCreatureAnimator => transform.parent.GetComponent<EmptyCreatureAnimator>();

    
}
