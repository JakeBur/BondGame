﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[System.Serializable]
public class CreatureAIContext : MonoBehaviour
{
    #region Behavior Tree Context

    [Header("Objects")]
    public GameObject player;
    public GameObject targetEnemy;
    public List<GameObject> enemyList;
    public GameObject cleverItem; //interesting items, only for clever creatures
    public GameObject foundFood; //Used in Hearty personality to find food
    public Transform creatureTransform;
    public Rigidbody rb;
    public ActiveCreatureData creatureStats;
    public creatureData creatureBaseStats;
    public NavMeshAgent agent;
    public GameObject backFollowPoint;
    public GameObject followPoint;
    public GameObject abilitySpawner;
    public CreatureAnimator animator;
    public GameObject interactRadius;
    public CooldownSystem cooldownSystem => GetComponent<CooldownSystem>();
    public creatureAttackBase basicCreatureAttack;
    public GameObject PetalCone;
    
    public List<GameObject> possiblePOIs = new List<GameObject>();
    
    public GameObject targetPOI;
    

    
    [Header("Bools")]
    public bool isWild;
    public bool isInPlayerRadius;
    public bool isInPlayerTrail;
    public bool isNoticed;
    public bool isAbilityTriggered;
    public bool wanderIdling = false;
    public bool cleverIgnoreItems = false;
    public bool isActive;
    public bool isHit;
    public bool inCombat;
    public bool autoAttack;
    public bool enthusiasmInteracted;
    public bool hasReacted;

    [Header("Misc.Numbers")]
    public float playerSpeedToScare;
    public int lastTriggeredAbility;
    public float enemyDetectRange;
    public float itemDetectRange; //range for detecting interesting items, only for clever creatures
    public float wanderDistance; //how far from starting location the creature can wander
    public float wanderIdleDuration;
    public float wanderIdleTimer;
    public Vector3 wanderDestination;
    public Vector3 wildStartingLocation;
    public float stealDuration;
    public float stealTimer;

    public float attention;
    public float boredom;
    public float tiredness;

    public float meterRate;


    private int debugNumber;
    private CreatureDebugText debugText;
    #endregion

    [Header("UI")]
    public Sprite icon; //done in creature spawner
    public Sprite ability1Icon;
    public Sprite ability2Icon;
    

    private void Awake()
    {
        creatureTransform = transform;
        wildStartingLocation = creatureTransform.position;
        animator = GetComponent<CreatureAnimator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        enemyList = new List<GameObject>();
       
        // GameObject temp = GameObject.FindGameObjectWithTag("CreatureDebugText");
        // debugText = temp.GetComponent<CreatureDebugText>();
        // debugText.creaturesDebug.Add("");
        // debugNumber = debugText.creaturesDebug.Count - 1;
        if(isWild){
            lastTriggeredAbility = 0;
        }



        resetStealTimer();
    }

    private void Start() 
    {
        player = PersistentData.Instance.Player;
        followPoint = GameObject.FindGameObjectWithTag("FrontFollowPoint");
        backFollowPoint = GameObject.FindGameObjectWithTag("BackFollowPoint");
    }

    public void GetActiveCreatureData(){
        creatureStats = GetComponent<ActiveCreatureData>();
    }

    private void FixedUpdate() {
        if(attention > 0)
        {
            attention -= Time.deltaTime * meterRate;
        }

        if(boredom < 100)
        {
            boredom += Time.deltaTime * meterRate;
        }


    }


    public void doMovement(float moveSpeed){
       //rb.velocity = (creatureTransform.transform.rotation * Vector3.forward * moveSpeed);
    }

    // public void doRotation(float rotationSpeed, Quaternion desiredLook) {
    //     creatureTransform.rotation = Quaternion.Slerp(creatureTransform.rotation, desiredLook, Time.deltaTime * rotationSpeed); //10 is rotation speed - might want to change later
    // }

    // public void doLookAt(Vector3 position){
    //     creatureTransform.transform.LookAt(position, Vector3.up);
    //     rb.velocity = (creatureTransform.transform.rotation * Vector3.forward * creatureStats.statManager.stats[ModiferType.MOVESPEED].modifiedValue);
    // }


    public void resetStealTimer() {
        stealTimer = 0;
        stealDuration = Random.Range(2f, 3f);
    }
    
    public void updateDebugText(string name) {
        // debugText.creaturesDebug[debugNumber] = gameObject.name + " : " + name + "\n";
    }

    public void DoDamage(){
        //fragaria's abilities base damage +* power modifier +* statManager damage modifier 

    }

    public void react(Reaction _reaction)
    {

    }


}

public enum Reaction
{
    HAPPY,
    SAD,
    EXCITED,
    SCARED
}
