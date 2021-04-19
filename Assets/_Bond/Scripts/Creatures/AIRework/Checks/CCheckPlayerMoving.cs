using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckPlayerMoving : BTChecker
{
    public CCheckPlayerMoving(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if(context.player.GetComponent<PlayerController>().inputs.rawDirection != Vector2.zero)
        {
            return NodeState.SUCCESS;
        } else 
        {
            return NodeState.FAILURE;
        }
    }
}
