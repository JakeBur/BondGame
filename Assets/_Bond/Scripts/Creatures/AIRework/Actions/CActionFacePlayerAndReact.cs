using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionFacePlayerAndReact : BTLeaf
{

    private NavMeshAgent agent;
    creatureAttackBase attack;
    private float moveSpeed = 15f;
    private float maxDist;

    public CActionFacePlayerAndReact(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
        agent = context.agent;
    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {
        context.hasReacted = true;
    }

    public override NodeState Evaluate() 
    {
        agent.ResetPath();
        context.creatureTransform.LookAt(context.player.transform.position);
        context.animator.Wave();
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
