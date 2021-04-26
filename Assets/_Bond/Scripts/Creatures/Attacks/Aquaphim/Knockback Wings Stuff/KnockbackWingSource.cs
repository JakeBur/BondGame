using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnockbackWingSource : MonoBehaviour
{

    public float radius = 50.0F;
    public float power = 5.0F;
    private Transform source;
    // Start is called before the first frame update
    private void Awake() 
    {
        source = gameObject.transform;
        StartCoroutine(doKnockback(1f, 4));
        Destroy(gameObject, 4.1f);
    }

    IEnumerator doKnockback(float duration, int count)
    {
        Vector3 explosionPos = source.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        int currentCount = 0;
        while(currentCount < count)
        {
            foreach (Collider hit in colliders)
            {
                if(hit.transform.tag == "Enemy")
                {
                    Rigidbody rb =  hit.GetComponent<EnemyAIContext>().rb;
                    // hit.GetComponent<EnemyAIContext>().rb;
                    NavMeshAgent agent = hit.GetComponent<EnemyAIContext>().agent;
                    // agent.isStopped = false;
                    Debug.Log(hit);

                    
                    rb.AddExplosionForce(power, explosionPos, radius, 1.0F, ForceMode.Impulse);
                    yield return new WaitForSeconds(0.1f);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    // agent.isStopped = true;
                }
            }
            // yield return new WaitForSeconds(duration);
            currentCount++;
        }
    }
}
