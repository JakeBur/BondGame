using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionInteractWithPOI : BTLeaf
{
    public CActionInteractWithPOI(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
        
    }

    protected override void OnEnter()
    {
        context.animator.InteractPOI(context.targetPOI.tag);
        context.agent.ResetPath();
    }

    protected override void OnExit()
    {
        context.targetPOI = null;
        context.boredom = 0;
        
    }

    public override NodeState Evaluate()
    {
        // Debug.Log("Interacting with POI");
        if(context.animator.isInteractPOI)
        {
            return NodeState.RUNNING;
        } else 
        {
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
        // OnParentExit();
        // return NodeState.FAILURE;
    }
}
