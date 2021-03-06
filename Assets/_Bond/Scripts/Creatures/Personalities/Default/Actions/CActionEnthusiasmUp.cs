using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionEnthusiasmUp : BTLeaf
{
    public CActionEnthusiasmUp(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Play anim
        context.animator.PlayerNoticed();
        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        
        if(true) 
        { //if animation done, have to add that 
            context.enthusiasmInteracted = false;
            context.creatureStats.statManager.setStat(ModiferType.CURR_ENTHUSIASM, context.creatureStats.statManager.getStat(ModiferType.MAX_ENTHUSIASM) * 0.25f);
            //Update the creature's Enthusiasm Bar
            context.creatureTransform.gameObject.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();

            OnParentExit();
            return NodeState.SUCCESS;
        }
        
    }
}
