// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionFollowPlayer : BTLeaf
{
    private NavMeshAgent agent;
    //private float moveSpeed = 5f;
    private float angularSpeed = 300; //deg/s
    private float acceleration = 100f; //max accel units/sec^2
    PlayerController playerController;
    Vector3 playerRight;
    float distToWanderDestination;


    private bool ending;
    private float waitLength = 0.2f;
    private float waitStartTime;

    public CActionFollowPlayer(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;

        agent = context.agent;
        //ObjToFollow = captCreature.followPoint;
        agent.autoBraking = true;
        agent.autoRepath = false;
        agent.acceleration = acceleration;

        //agent.speed = moveSpeed;
    }

    protected override void OnEnter()
    {
        // context.animator.interactPOIFalse();
        playerController = context.player.GetComponent<PlayerController>();
        agent.speed = context.creatureStats.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
        agent.angularSpeed = angularSpeed;

        agent.isStopped = false;
        // distToWanderDestination = Vector3.Distance(context.wanderDestination, context.creatureTransform.position);
        ending = false;
        //Get point in code in front of creature
        //Vector3 chosenPoint = RandomPointInCircle(context.creatureTransform, 5, Random.Range(-75,75));
        //Add that point to the player location to influence the direction
        //Vector3 influencedPoint = chosenPoint + context.player.transform.position;
        
        float angleToPlayer = Vector3.SignedAngle(context.creatureTransform.forward, (context.player.transform.position - context.creatureTransform.position).normalized, Vector3.up);
        float finalAngle = (Random.Range(-45,45) + angleToPlayer) / 2;
        Vector3 influencedPoint = (Quaternion.Euler(new Vector3(0, finalAngle, 0)) * context.creatureTransform.forward) * context.wanderDistance + context.creatureTransform.position;
        
        //Go to the influenced point
        //agent.destination = influencedPoint;
        agent.destination = influencedPoint;
        context.wanderDestination = agent.destination;
        //agent.SetDestination(influencedPoint);
        
        
        

        // agent.SetDestination(context.player.transform.position);
        distToWanderDestination = Vector3.Distance(influencedPoint, context.creatureTransform.position);
    }

    protected override void OnExit()
    {
        // context.doMovement(0f);

        //This might break things when the player stops moving?
        
        agent.ResetPath();
    }

    public override NodeState Evaluate()
    {
        // Debug.Log("Has Path " + agent.hasPath);
        // Debug.Log("is stopped" + agent.isStopped);
        //agent.SetDestination(context.backFollowPoint.transform.position);
        //distToWanderDestination = Vector3.Distance(context.player.transform.position, context.creatureTransform.position);
        /*if(!ending && Vector3.Distance(context.wanderDestination, context.creatureTransform.position) <= distToWanderDestination/2)
        {
            ending = true;
            waitStartTime = Time.time;
        }*/

        if(Vector3.Distance(context.wanderDestination, context.creatureTransform.position) <= distToWanderDestination/2)//ending && Time.time > waitStartTime + waitLength
        {
            // Creature successfully got halfway to wander destination
            //Debug.Log("Followed Halfway");
            OnParentExit();
            return NodeState.SUCCESS;
        } else 
        {
            //Debug.Log("Following");
            return NodeState.RUNNING;
        }
    }

    
    float linearMap(float value, float inputLow, float inputHigh, float outputLow, float outputHigh) 
    {
        return outputLow + (outputHigh - outputLow) * (value - inputLow) / (inputHigh - inputLow);
    }

    Vector3 RandomPointInCircle(Transform trans, float radius, float angle){
        float rad = angle * Mathf.Deg2Rad;
        Vector3 position = trans.right * Mathf.Sin( rad ) + trans.forward * Mathf.Cos( rad );
        return trans.position + position * radius;
    }
}

/*
        playerRight = playerController.transform.right * linearMap(context.attention, 0, 100, 5, 3);
        if(Random.Range(0,2) == 1)
        {   
            playerRight *= -1;
        }

        playerRight += context.player.transform.position;
*/