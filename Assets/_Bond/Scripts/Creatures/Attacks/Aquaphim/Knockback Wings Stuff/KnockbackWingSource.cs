using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnockbackWingSource : MonoBehaviour
{

    public float radius = 10.0F;
    public float power = 5.0F;
    private Transform source;
    // Start is called before the first frame update
    private void Awake() 
    {
        source = gameObject.transform;
        Debug.Log(source);
        StartCoroutine(doKnockback(.5f, 8));
        Destroy(gameObject, 4.5f);
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
                if(hit.tag == "Enemy")
                {
                    // Enemy = hit.GetComponent<Enemy>();
                    Rigidbody rb =  hit.GetComponent<EnemyAIContext>().rb;
                    // rb.velocity = Vector3.zero;
                    // rb.angularVelocity = Vector3.zero;
                    NavMeshAgent agent = hit.GetComponent<EnemyAIContext>().agent;
                    agent.isStopped = true;
                    // hit.GetComponent<EnemyAIContext>().rb;
                    agent.isStopped = false;
                    Debug.Log(hit);
                    rb.AddExplosionForce(power, explosionPos, radius, 1.0F, ForceMode.Impulse);
                    yield return new WaitForSeconds(duration);
                }
            }
            // yield return new WaitForSeconds(duration);
            currentCount++;
        }
    }
}
