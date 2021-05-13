//Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckWildFrozen : BTChecker
{

    public CCheckWildFrozen(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.creatureFrozen)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
