using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionPassiveEnthusiasmGain : BTLeaf
{
    float enthusiasmGainMultiplier = 10f;
    public CActionPassiveEnthusiasmGain(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Play anim

        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        
        if(true) 
        { //if animation done, have to add that 
            
            if(context.creatureStats.statManager.getStat(ModiferType.CURR_ENTHUSIASM) < context.creatureStats.statManager.getStat(ModiferType.MAX_ENTHUSIASM))
            {
                context.creatureStats.statManager.setStat(ModiferType.CURR_ENTHUSIASM, context.creatureStats.statManager.getStat(ModiferType.CURR_ENTHUSIASM) + Time.deltaTime * enthusiasmGainMultiplier);
                //Update creature's Enthusiasm Bar
                context.creatureTransform.gameObject.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();
            }
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
    }
}
