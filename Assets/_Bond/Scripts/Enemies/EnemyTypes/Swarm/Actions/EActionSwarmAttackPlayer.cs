using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionSwarmAttackPlayer : BTLeaf
{
    float delayTimer = 0;

    public EActionSwarmAttackPlayer(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        enemyContext = _context;
        name = _name;
    }

    protected override void OnEnter()
    {
        delayTimer = 0;
        //Play attack anim
        enemyContext.animator.Attack();
    }

    protected override void OnExit()
    {
        delayTimer = 0;
    }

    public override NodeState Evaluate() 
    {
        if(enemyContext.animator.inAttack)
        {
            return NodeState.RUNNING;
        } else 
        {
            delayTimer += Time.deltaTime;
            //Delay after attack
            if(delayTimer >= enemyContext.delayBetweenAttacks)
            {
                OnParentExit();
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING;
        }
    }
}
