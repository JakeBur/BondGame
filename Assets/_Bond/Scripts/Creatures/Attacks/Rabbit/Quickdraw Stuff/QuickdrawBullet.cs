using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickdrawBullet : MonoBehaviour
{
    private float damage;
    private Buff debuff;
    GameObject target;
    Rigidbody rigidBody;
    float speed = 10;
    private bool isHoming = true;

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

    public void setTarget(GameObject _target, float _speed, float _damage)
    {
        target = _target;
        transform.LookAt(target.transform.position);
        speed = _speed;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Enemy")
        {
            EnemyAIContext enemyAIContext = other.transform.GetComponent<EnemyAIContext>();
            enemyAIContext.statManager.TakeDamage(damage, ModiferType.RANGED_RESISTANCE);
            enemyAIContext.healthUIUpdate();
            Destroy(gameObject);
        }    
    }
}
