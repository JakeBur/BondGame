using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECheckIfRangedOnCD : BTChecker
{
    public ECheckIfRangedOnCD(string _name, EnemyAIContext _context) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    public override NodeState Evaluate()
    {
        if(enemyContext.attacking)
        {
            return NodeState.FAILURE;
        }
        if (enemyContext.attackCD > 0 || 
            enemyContext.EncounterManager.numberOfCurrRangedAttackers >=  enemyContext.EncounterManager.maxCurrRangedAttackers)
        {
            return NodeState.SUCCESS;
        }
        enemyContext.attacking = true;
        enemyContext.EncounterManager.numberOfCurrRangedAttackers++;
        return NodeState.FAILURE;
    }
}
