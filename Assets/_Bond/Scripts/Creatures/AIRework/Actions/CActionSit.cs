﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionSit : BTLeaf
{

    float endTime;
    float timer;

    public CActionSit(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        context.animator.Sit();
        endTime = Time.time + Random.Range(5f,10f);
        timer = Time.time;
    }

    protected override void OnExit()
    {
        context.tiredness = 0;
    }

    public override NodeState Evaluate() 
    {
        Debug.Log("SIT");
        if(timer >= endTime)
        {
            OnParentExit();
            return NodeState.SUCCESS;
        }
        if(context.hasReacted)
        {
            context.animator.Sit();
        }
        timer += Time.deltaTime;
        return NodeState.RUNNING;
    }
}
