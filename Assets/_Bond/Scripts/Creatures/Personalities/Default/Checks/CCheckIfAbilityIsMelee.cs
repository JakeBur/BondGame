using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCheckIfAbilityIsMelee : BTChecker
{
    public CCheckIfAbilityIsMelee(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if(context.lastTriggeredAbility >= 0)
        {
           
            if(context.creatureStats.abilities[context.lastTriggeredAbility] is CreatureAttackMelee)
            {
               
                return NodeState.SUCCESS;
            }
        }
        
        return NodeState.FAILURE;
    }
}
