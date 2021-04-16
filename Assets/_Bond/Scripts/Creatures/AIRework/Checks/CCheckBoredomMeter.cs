using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckBoredomMeter : BTChecker
{
    public CCheckBoredomMeter(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
       
        if(context.boredom > 70)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
