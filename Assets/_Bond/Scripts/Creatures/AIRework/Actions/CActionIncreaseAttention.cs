using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionIncreaseAttention : BTLeaf
{
    public CActionIncreaseAttention(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        context.attention += 100;
        if(context.attention >150) 
        {
            context.attention = 150;
        }
        // context.player.GetComponent<PlayerController>().didWhistle = false;
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
