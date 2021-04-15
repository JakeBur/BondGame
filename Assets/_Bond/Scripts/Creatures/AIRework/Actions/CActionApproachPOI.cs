using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionApproachPOI : BTLeaf
{
    private NavMeshAgent agent;
    float tempTurnSpeed;
    bool justSetPath;

    public CActionApproachPOI(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
        agent = context.agent;
        agent.autoRepath = true;
        tempTurnSpeed = agent.angularSpeed;
    }

    protected override void OnEnter()
    {
        context.targetPOI = null;
    }

    protected override void OnExit()
    {
        justSetPath = false;
        context.agent.angularSpeed = tempTurnSpeed;
        context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED);
    }

    public override NodeState Evaluate()
    {
        if(justSetPath)
        {
            justSetPath = false;
            return NodeState.RUNNING;
        }
        if(context.animator.isInteractPOI)
        {
            OnParentExit();
            return NodeState.SUCCESS;
        } 

        context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED) / 4;
        context.agent.angularSpeed = tempTurnSpeed/8;

        if(context.targetPOI == null && context.possiblePOIs.Count > 0)
        {
            context.targetPOI = context.possiblePOIs[Random.Range(0, context.possiblePOIs.Count)];
            agent.destination = context.targetPOI.transform.position;
            justSetPath = true;
            return NodeState.RUNNING;
        } else if(!agent.hasPath)
        {
            OnParentExit();
            return NodeState.FAILURE;   
        }
        
        if(context.targetPOI != null)
        {
            if(Vector3.Distance(context.creatureTransform.position, context.targetPOI.transform.position) < 3)
            {
                OnParentExit();
                return NodeState.SUCCESS;
            } else 
            {
                return NodeState.RUNNING;
            }
        }

        OnParentExit();
        return NodeState.FAILURE;
    }
}
