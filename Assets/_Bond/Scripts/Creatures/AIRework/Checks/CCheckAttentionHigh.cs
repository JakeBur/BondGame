using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckAttentionHigh : BTChecker
{
    public CCheckAttentionHigh(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }
    public override NodeState Evaluate()
    {
        if(context.attention >  50)
        {
            return NodeState.SUCCESS;
        } else 
        {
            return NodeState.FAILURE;
        }
    }
}
