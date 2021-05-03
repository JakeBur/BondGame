using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CCheckInCombat : BTChecker
{

    public CCheckInCombat(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.inCombat)
        {
            context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED);
            context.agent.angularSpeed = 700;
            return NodeState.SUCCESS;
        }
        if(context.lastTriggeredAbility >= 0)
        {
            context.lastTriggeredAbility = -1;
            PersistentData.Instance.UI.GetComponent<hudUI>().OnAbilityFail();
        }
        return NodeState.FAILURE;
    }
}
