using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe rename this class?
public class CCheckDistanceToTarget : BTChecker
{
    public CCheckDistanceToTarget(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(context.creatureTransform.position, context.targetEnemy.transform.position);
       
        if(context.creatureStats.abilities[context.lastTriggeredAbility] is CreatureAttackMelee)
        {
            CreatureAttackMelee attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
            
            if (distance < attack.maxDistanceToEnemy) return NodeState.FAILURE; 
        } 
        else if(context.creatureStats.abilities[context.lastTriggeredAbility] is CreatureAttackRanged) 
        {
            CreatureAttackRanged attack = (CreatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
            if (distance < attack.maxDistanceToEnemy) return NodeState.FAILURE; 
        }
        
        return NodeState.SUCCESS;
        
    }
}
