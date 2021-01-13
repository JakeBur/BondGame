﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionPlayDeathAnim : BTLeaf
{
    public EActionPlayDeathAnim(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    protected override void OnEnter()
    {
        //Play death anim
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Destroy enemy
        enemyContext.activeEnemyData.DestroyEnemy();
        return NodeState.SUCCESS;
    }
}
