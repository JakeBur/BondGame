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
            context.playerMoving = true;
        }
        
        if(context.player.GetComponent<PlayerController>().inputs.rawDirection == Vector2.zero && 
        Vector3.Distance(context.creatureTransform.position, context.player.transform.position) < 5)
        {
            context.playerMoving = false;
        }


        if(context.playerMoving)
        {
            return NodeState.SUCCESS;
        } else 
        {
            return NodeState.FAILURE;
        }
    }
}
