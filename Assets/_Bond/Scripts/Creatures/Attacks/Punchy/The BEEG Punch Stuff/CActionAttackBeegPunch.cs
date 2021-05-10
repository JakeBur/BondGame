using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackBeegPunch : BTLeaf
{

    CreatureAttackMelee attack;
    public CActionAttackBeegPunch(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play amim
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.targetEnemy.GetComponent<EnemyAIContext>().statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        Vector3 moveDirection = context.targetEnemy.transform.position - context.creatureTransform.transform.position;
        Rigidbody rb =  context.targetEnemy.GetComponent<EnemyAIContext>().rb;
        rb.AddForce(moveDirection.normalized * 50f);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility )
        { //if animation done, have to add that 
      
            context.player.GetComponent<PlayerController>().PutOnCD();
            // Debug.Log("Ability Id: ");
            // Debug.Log(context.creatureStats.abilities[context.lastTriggeredAbility].id);
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }

}
