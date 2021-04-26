using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionFacePlayerAndReact : BTLeaf
{
    private NavMeshAgent agent;

    public CActionFacePlayerAndReact(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
        agent = context.agent;
    }

    protected override void OnEnter()
    {
        agent.isStopped = true;
        context.creatureTransform.LookAt(context.player.transform.position);
        context.animator.PlayerNoticed();
        context.isReacting = true;
    }

    protected override void OnExit()
    {
        context.hasReacted = true;
        agent.isStopped = false;        
        context.animator.interactPOIFalse();
        context.isReacting = false;
    }

    public override NodeState Evaluate() 
    {
        if(!context.animator.isPlayerNoticed){
            OnParentExit();
            return NodeState.SUCCESS;   
        }
        return NodeState.RUNNING;
    }
}
