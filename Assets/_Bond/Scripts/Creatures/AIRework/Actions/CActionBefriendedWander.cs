using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionBefriendedWander : BTLeaf
{
    NavMeshAgent agent;
    float tempTurnSpeed;
    private float distToWanderDestination;
    Vector3 playerToCreatureVector;
    bool justEntered;


    public CActionBefriendedWander(string _name, CreatureAIContext _context ) : base(_name, _context)
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
        context.agent.angularSpeed = tempTurnSpeed;
        
        context.wanderDestination = RandomPointInCircle(context.creatureTransform, context.wanderDistance, Random.Range(-60,60));
        playerToCreatureVector = (context.wanderDestination - context.player.transform.position).normalized;

        float distToPlayer = Vector3.Distance(context.player.transform.position, context.wanderDestination);

        if(distToPlayer < 0)
        {
            playerToCreatureVector *= linearMap(distToPlayer, 0, 10, 0, 3);
            //playerToCreatureVector *= linearMap(distToPlayer, 0, 10, 0, 3);
        } 
        else 
        {
            playerToCreatureVector *= -1;
            playerToCreatureVector *= 3 * context.wanderDistance * context.wanderDistanceCurve.Evaluate(linearMap(distToPlayer, 10, 20, 0, 1));
            //playerToCreatureVector *= linearMap(distToPlayer, 10, 20, 0, 3);
        }

        context.wanderDestination += playerToCreatureVector;
        
        //This finds the closest point on the navmesh to the random location
        // NavMeshHit hit;
        // if (NavMesh.SamplePosition(context.wanderDestination, out hit, context.wanderDistance, NavMesh.AllAreas))
        // {
        //     context.wanderDestination = hit.position;
        // }

        agent.destination = context.wanderDestination;

        distToWanderDestination = Vector3.Distance(context.wanderDestination, context.creatureTransform.position);
        justEntered = true;
    }

    protected override void OnExit()
    {
        agent.ResetPath();
    }

    public override NodeState Evaluate() 
    {
        // Debug.Log("has path : " + agent.hasPath);
        // Debug.Log("Path Status : " + agent.pathStatus);
        // Debug.Log("Path PENDING : " + agent.pathPending);
        // Debug.Log("Destination = " + agent.destination);

        if(context.inCombat)
        {
            context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED) / 2;
            context.agent.angularSpeed = 600;    
        } 
        else 
        {
            context.agent.speed = context.creatureStats.statManager.getStat(ModiferType.MOVESPEED) / 4;
            context.agent.angularSpeed = tempTurnSpeed/8;
        }
       

        if(context.tiredness < 100)
        {
            context.tiredness += Time.deltaTime * context.meterRate;
        }


        if(Vector3.Distance(context.wanderDestination, context.creatureTransform.position) <= distToWanderDestination/2)
        {
            // Creature got halfway to wander destination
            OnParentExit();
            return NodeState.SUCCESS;
        } else if(justEntered || agent.pathPending)
        {
            justEntered = false;
            return NodeState.RUNNING;
        } else if(!agent.hasPath)
        {
            OnParentExit();
            return NodeState.SUCCESS;   
        }

        return NodeState.RUNNING;
        
    }

    Vector3 RandomPointInCircle(Transform trans, float radius, float angle){
        float rad = angle * Mathf.Deg2Rad;
        Vector3 position = trans.right * Mathf.Sin( rad ) + trans.forward * Mathf.Cos( rad );
        return trans.position + position * radius;
    }

    float linearMap(float value, float inputLow, float inputHigh, float outputLow, float outputHigh) 
    {
        return outputLow + (outputHigh - outputLow) * (value - inputLow) / (inputHigh - inputLow);
    }
}
