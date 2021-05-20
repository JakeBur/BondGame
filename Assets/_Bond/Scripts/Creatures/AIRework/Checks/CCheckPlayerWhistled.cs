using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckPlayerWhistled : BTChecker
{
    public CCheckPlayerWhistled(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        
        // if(context.player.GetComponent<PlayerController>().didWhistle)
        // {
        //     Debug.Log("process whistle");
        //     return NodeState.SUCCESS;
        // } else 
        // {
        //     return NodeState.FAILURE;
        // }
        return NodeState.FAILURE;
    }
}
