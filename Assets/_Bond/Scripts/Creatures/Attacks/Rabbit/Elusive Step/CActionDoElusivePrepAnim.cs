﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionDoElusivePrepAnim : BTLeaf
{
    
    public CActionDoElusivePrepAnim(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        // RabbitAnimator animator = context.animator as RabbitAnimator;
        // if (animator == null)
        // {
        //     Debug.LogError("animator is not rabbit animator");
        // }
        // animator.ElusiveStepPose();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() {
        // if(context.animator.inElusiveStepPose)
        // {
        //     return NodeState.RUNNING;
        // } else
        // {
        //     OnParentExit();
        //     return NodeState.SUCCESS;
        // }
        return NodeState.SUCCESS;
    }
}
