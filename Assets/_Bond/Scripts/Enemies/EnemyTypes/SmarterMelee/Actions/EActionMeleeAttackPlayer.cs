using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionMeleeAttackPlayer : BTLeaf
{
    public EActionMeleeAttackPlayer(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        enemyContext = _context;
        name = _name;
    }

    protected override void OnEnter()
    {
      
        //Play attack anim
        enemyContext.animator.Attack();
    }

    protected override void OnExit()
    {
        enemyContext.attackCD = enemyContext.EncounterManager.currEnemyCount;
    }

    public override NodeState Evaluate() 
    {
        Debug.Log("ATTACKING");
        if(enemyContext.animator.inAttack)
        {
            return NodeState.RUNNING;
        } 
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
