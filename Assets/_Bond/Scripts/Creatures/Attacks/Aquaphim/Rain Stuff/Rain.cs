using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    private float damage;
    private Buff debuff;

    private void Awake() 
    {
        Destroy(gameObject, 4.1f);
    }

    public void setDamage(float _damage, Buff _debuff)
    {
        damage = _damage;
        debuff = _debuff;
    }
    IEnumerator DoRainDamage(float damageDuration, int damageCount, float damageAmount, Collider other)
    {
        EnemyAIContext enemyAIContext = other.transform.GetComponent<EnemyAIContext>();
        enemyAIContext.onRain = true;
        int currentCount = 0;
        while(currentCount < damageCount)
        {
            enemyAIContext.statManager.TakeDamage(damageAmount, ModiferType.RANGED_RESISTANCE);
            enemyAIContext.healthUIUpdate();
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
        enemyAIContext.onRain = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Enemy" && !other.transform.GetComponent<EnemyAIContext>().onRain)
        {
            StartCoroutine(DoRainDamage(0.25f, 16, damage, other));
        }    
    }
}
