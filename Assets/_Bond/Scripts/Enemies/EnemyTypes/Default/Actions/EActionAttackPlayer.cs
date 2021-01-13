﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionAttackPlayer : BTLeaf
{
    public EActionAttackPlayer(string _name, EnemyAIContext _context ) : base(_name, _context)
    {

    }

    protected override void OnEnter()
    {
        //Play attack anim
        enemyContext.animator.Attack();
    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        Debug.Log("Attacking player");
        return NodeState.SUCCESS;
    }
}
