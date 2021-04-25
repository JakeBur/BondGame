using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackWaterShield : BTLeaf
{
    private float damage;
    private Buff shield;
    private bool onRain;
    creatureAttackUtility attack;

    public CActionAttackWaterShield(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Look at the player before playing animation
        context.creatureTransform.LookAt(context.player.transform.position);

        attack = (creatureAttackUtility) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play anim
        context.animator.Attack1();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        PlayerController pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        pc.stats.AddBuff(attack.abilityBuff);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
    }
}
