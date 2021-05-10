﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShotBullet : MonoBehaviour
{
    private float damage;
    private Buff debuff;
    GameObject target;
    Rigidbody rigidBody;
    float speed = 10;
    private bool isHoming = false;

    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    public void setDamage(float _damage, Buff _debuff)
    {
        damage = _damage;
        debuff = _debuff;
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
            
            rigidBody.velocity = (transform.rotation*Vector3.forward*speed);
        }
    }
    IEnumerator DoRainDamage(float damageDuration, int damageCount, float damageAmount, Collider other)
    {
        int currentCount = 0;
        while(currentCount < damageCount)
        {
            other.transform.GetComponent<EnemyAIContext>().statManager.TakeDamage(damageAmount, ModiferType.RANGED_RESISTANCE);
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
    }

    public void setTarget(GameObject _target, float _speed, float _damage, bool _isHoming)
    {
        target = _target;
        transform.LookAt(target.transform.position);
        speed = _speed;
        isHoming = _isHoming;
    }

    private void OnTriggerEnter(Collider other) {

        if(other.transform.tag == "Enemy")
        {
            StartCoroutine(DoRainDamage(1f, 4, damage, other));
            Destroy(gameObject);
        }    
        
    }
}