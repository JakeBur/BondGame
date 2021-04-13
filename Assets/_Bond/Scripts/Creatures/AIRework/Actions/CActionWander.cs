using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionWander : BTLeaf
{
    private NavMeshAgent agent;
    private float distToWanderDestination;
    float tempTurnSpeed;


    public CActionWander(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
        agent = context.agent;
        tempTurnSpeed = agent.angularSpeed;
    }

    protected override void OnEnter()
    {
        agent.isStopped = false;
        context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED) / 4;
        context.agent.angularSpeed = tempTurnSpeed/8;
        context.wanderDestination = RandomPointInCircle(context.creatureTransform, context.wanderDistance, Random.Range(-75,75));
        //This finds the closest point on the navmesh to the random location
        NavMeshHit hit;
        if (NavMesh.SamplePosition(context.wanderDestination, out hit, context.wanderDistance, NavMesh.AllAreas)){
            context.wanderDestination = hit.position;
        }
        agent.destination = context.wanderDestination;
        distToWanderDestination = Vector3.Distance(context.wanderDestination, context.creatureTransform.position);
    }

    protected override void OnExit()
    {
        context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED);
        context.agent.angularSpeed = tempTurnSpeed;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("WANDER is stopped:" + agent.isStopped);
        
        context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED) / 4;
        context.agent.angularSpeed = tempTurnSpeed/8;

        if(context.tiredness < 100)
        {
            context.tiredness += Time.deltaTime * context.meterRate;
        }


        if(Vector3.Distance(context.wanderDestination, context.creatureTransform.position) <= distToWanderDestination/2)
        {
            // Creature got halfway to wander destination
            OnParentExit();
            return NodeState.SUCCESS;
        } else if(!agent.hasPath)
        {
            OnParentExit();
            return NodeState.SUCCESS;
        } else if(agent.isStopped == true && !context.animator.isWaving) 
        {
            OnParentExit();
            return NodeState.SUCCESS;
        }
            else{
            // Still wandering
            return NodeState.RUNNING;
        }
    }



    Vector3 RandomPointInCircle(Transform trans, float radius, float angle){
        float rad = angle * Mathf.Deg2Rad;
        Vector3 position = trans.right * Mathf.Sin( rad ) + trans.forward * Mathf.Cos( rad );
        return trans.position + position * radius;
    }
}
