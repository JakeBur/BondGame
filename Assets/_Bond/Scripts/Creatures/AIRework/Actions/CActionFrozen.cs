using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionFrozen : BTLeaf
{
    public CActionFrozen(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Maybe play scared anim?
    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        //Do nothing cause this boi frozen
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
