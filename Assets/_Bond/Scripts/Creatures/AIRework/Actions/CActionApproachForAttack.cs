using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionApproachForAttack : BTLeaf
{

    private NavMeshAgent agent;
    CreatureAttackBase attack;
    private float moveSpeed = 15f;
    private float maxDist;

    public CActionApproachForAttack(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
        agent = context.agent;
    }

    protected override void OnEnter()
    {
        // Debug.Log("Enter Approach for attack");
        if(context.creatureStats.abilities[context.lastTriggeredAbility] is CreatureAttackMelee) 
        {
            CreatureAttackMelee _attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
            maxDist = _attack.maxDistanceToEnemy;
            // Debug.Log("melee max dist" + _attack.maxDistanceToEnemy);
           
        } else if(context.creatureStats.abilities[context.lastTriggeredAbility] is CreatureAttackRanged) 
        {
            CreatureAttackRanged _attack = (CreatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];   
            maxDist = _attack.maxDistanceToEnemy;
            // Debug.Log("ranged max dist" + _attack.maxDistanceToEnemy);
        }
        agent.speed = moveSpeed;
        // agent.stoppingDistance = attack.maxDistanceToEnemy;
    }

    protected override void OnExit()
    {
        agent.speed = context.creatureStats.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
        agent.stoppingDistance = 0f;
        agent.ResetPath();
    }

    public override NodeState Evaluate() {
        // Debug.Log("APROACH FOR ATTACK");
        agent.destination = context.targetEnemy.transform.position;
        float distance = Vector3.Distance(context.creatureTransform.position, context.targetEnemy.transform.position);
        // Debug.Log("Approaching Enemy : " + distance+ " MAX dist: " + maxDist);
        // Debug.Log("Distance : " + distance + " maxDist : " + attack.maxDistanceToEnemy);
        if(distance < maxDist)
        {
            // Made it to Enemy
            OnParentExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to Enemy
            context.updateDebugText(name);
            return NodeState.RUNNING;
        }
    }
}
