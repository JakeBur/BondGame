﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECheckPlayerNoticedBefore : BTChecker
{
    public ECheckPlayerNoticedBefore(string _name, EnemyAIContext _context) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    public override NodeState Evaluate()
    {
        if (enemyContext.playerNoticedBefore && !enemyContext.animator.inSpawn)
        {
            return NodeState.FAILURE;
        }
        return NodeState.SUCCESS;
    }
}
