﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    GameObject target;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    float speed = 10;
    public float damage = 50;
    public bool isHoming = false;

    void Start()
    {
        
    }

    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if(isHoming) 
            {
                transform.LookAt(target.transform.position);
            }
            rigidBody.velocity = (transform.forward*speed);
        }
    }

    public void setTarget(GameObject _target, float _speed, float _damage, bool _isHoming)
    {
        target = _target;
        if(target != null)
        {
            transform.LookAt(target.transform.position);
        }
        
        speed = _speed;
        damage = _damage;
        isHoming = _isHoming;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Player")
        {
            other.transform.GetComponent<StatManager>().TakeDamage(damage, ModiferType.RANGED_RESISTANCE);
            other.gameObject.GetComponent<PlayerController>().isHit = true;
            other.gameObject.GetComponent<PlayerController>().DeathCheck();
            Destroy(gameObject);
        }    
    }
}
