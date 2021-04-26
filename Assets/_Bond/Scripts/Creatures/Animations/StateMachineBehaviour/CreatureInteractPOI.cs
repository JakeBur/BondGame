using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInteractPOI : CreatureSMB
{

    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CreatureAnimator animator = GetCreatureAnimator( unityAnimator );

        animator.SMBInteractPOIExit();
    }
}
