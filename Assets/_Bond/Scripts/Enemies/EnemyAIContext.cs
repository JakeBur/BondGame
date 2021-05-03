﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

[System.Serializable]
public class EnemyAIContext : MonoBehaviour
{
    #region Behavior Tree Context

    //[Header("Main Stats")]
    public StatManager statManager => GetComponent<StatManager>();

    [Header("Objects")]
    public GameObject player;
    public Transform enemyTransform;
    public string enemyType => GetComponent<EnemyAI>().currentEnemyType.enemyName;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public EnemyAnimator animator;
    public EncounterManager EncounterManager; //to tell it when to spawn the next enemy
    public GameObject attackSpawner;
    public Canvas enemyUI;
    public Slider healthSlider;
    public ParticleSystem hitVFX;
    
    [Header("Bools")]
    public bool isInPlayerRadius;
    public bool playerNoticedBefore;
    public bool playerNoticed;
    public bool isIdling = false;
    public bool tookDamage = false;
    public bool attacking = false;
    public bool onRain = false;

    [Header("Misc.Numbers")]
    public float delayBetweenAttacks;
    public Vector3 startingLocation;
    private float lastCheckedHealth;
    public float lastDamageTaken;
    public GameObject goldPrefab;
    public float attackCD;
    public float retreatDist;

    public float hitstunDuration;

    public float movementSpeed;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    #endregion

    private void Awake()
    {
        enemyTransform = transform;
        startingLocation = enemyTransform.position;
        animator = GetComponent<EnemyAnimator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        player = PersistentData.Instance.Player;
        //player = GameObject.FindGameObjectWithTag("Player");
        lastCheckedHealth = statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;
    }

    private void FixedUpdate() 
    {
        //Check for damage and update health UI accordingly
        if(statManager.stats[ModiferType.CURR_HEALTH].modifiedValue < lastCheckedHealth)
        {
            tookDamage = true;
            lastDamageTaken = lastCheckedHealth - statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;
            // Debug.Log("lastCheckedHealth = " + lastCheckedHealth);
            // Debug.Log("Took " + lastDamageTaken + " Damage");
            healthUIUpdate();
            lastCheckedHealth = statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;

            hitVFX.Play();
        }

        enemyUI.transform.rotation = Camera.main.transform.rotation; //makes ui always face camera

        if(attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }

    }

    // public void doMovement(float moveSpeed)
    // {
    //     rb.velocity = (enemyTransform.transform.rotation * Vector3.forward * moveSpeed);
    // }

    // public void doRotation(float rotationSpeed, Quaternion desiredLook) 
    // {
    //     enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, desiredLook, Time.deltaTime * rotationSpeed); //10 is rotation speed - might want to change later
    // }

    // public void doLookAt(Vector3 position)
    // {
    //     enemyTransform.transform.LookAt(position, Vector3.up);
    //     rb.velocity = (enemyTransform.transform.rotation * Vector3.forward * statManager.stats[ModiferType.MOVESPEED].modifiedValue);
    // }

    public void DestroyEnemy()
    {
        animator.Death();
        player.GetComponent<LevelUpSystem>().GainXp(5); //probably add something to determine enemy type or difficulty to adjust reward
        Destroy(gameObject);
    }

    void healthUIUpdate()
    {
        healthSlider.value = (statManager.stats[ModiferType.CURR_HEALTH].modifiedValue / statManager.stats[ModiferType.MAX_HEALTH].modifiedValue) * 100;
        // (stats.getStat(ModiferType.CURR_HEALTH) / stats.getStat(ModiferType.MAX_HEALTH)) * 100
        // Debug.Log("healthslider value = " + healthSlider.value);
        // Debug.Log("curr health = " + statManager.stats[ModiferType.CURR_HEALTH].modifiedValue);
        // Debug.Log("max health = " + statManager.stats[ModiferType.MAX_HEALTH].modifiedValue);
    }

    public void dropGold()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.goldCount++;
        //play a sound and make a particle or something of acorns flying out

        SFXPlayer.PlayOneShot(SFX.CollectMoneySFX, transform.position);
    }
}
