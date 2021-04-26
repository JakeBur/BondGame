
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionAttackPetalThrow : BTLeaf
{
    CreatureAttackRanged attack;
    private NavMeshAgent agent;
    public CActionAttackPetalThrow(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
        
        //Play anim
        FragariaAnimator animator = context.animator as FragariaAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not fragaria animator");
        }
        animator.PetalThrow();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.gameObject.transform.LookAt(context.targetEnemy.transform);
        context.gameObject.transform.rotation = new Quaternion(0, context.gameObject.transform.rotation.y, 0, context.gameObject.transform.rotation.w);
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnPetals(context.PetalCone, context.creatureTransform);
        if (context.enemyList != null)
        {
            foreach (GameObject enemy in context.enemyList)
            {
                context.abilitySpawner.GetComponent<AbilitySpawner>()
                    .SpawnProjectile(attack.projectile, enemy, attack.flyTime, attack.baseDamage);
            }
        }

        context.enemyList.Clear();
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility ) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
}
