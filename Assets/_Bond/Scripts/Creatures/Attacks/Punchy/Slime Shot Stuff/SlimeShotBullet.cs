using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShotBullet : MonoBehaviour
{
    private float damage;
    private Buff debuff;
    GameObject target;
    Rigidbody rigidBody;
    float speed = 10;
    public bool isHoming = false;
    public Renderer rend;

    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 8f);
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
            StartCoroutine(DoSlimeDamage(1f, 4, damage, other));
            rend = GetComponent<Renderer>();
            rend.enabled = false;
        }    
        
    }    
    IEnumerator DoSlimeDamage(float damageDuration, int damageCount, float damageAmount, Collider other)
    {
        int currentCount = 0;
        EnemyAIContext enemyAIContext = other.transform.GetComponent<EnemyAIContext>();
        while(currentCount < damageCount)
        {
            enemyAIContext.statManager.TakeDamage(damageAmount, ModiferType.RANGED_RESISTANCE);
            enemyAIContext.healthUIUpdate();
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
    }

}