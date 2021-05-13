using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionGoToPlayerForAbility : BTLeaf
{
    public CActionGoToPlayerForAbility(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED);
    }

    protected override void OnExit()
    {
        context.agent.ResetPath();
    }

    public override NodeState Evaluate() 
    {
        if(!context.wentToPlayerForAbility)
        {
            context.agent.SetDestination(context.player.transform.position);
            if(Vector3.Distance(context.player.transform.position, context.creatureTransform.position) <= 3)
            {
                context.wentToPlayerForAbility = true;
                OnParentExit();
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        } else 
        {
            return NodeState.SUCCESS;
        }

    }
}
