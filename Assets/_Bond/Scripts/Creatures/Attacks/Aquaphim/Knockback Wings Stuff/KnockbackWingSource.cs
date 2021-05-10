using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KnockbackWingSource : MonoBehaviour
{

    public float radius;
    public float power;
    private Transform source;
    // Start is called before the first frame update
    private void Awake() 
    {
        source = gameObject.transform;
        Debug.Log(source);
        Vector3 explosionPos = source.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        StartCoroutine(doKnockback(.5f, 8, explosionPos, colliders));
        Destroy(gameObject, 4.5f);
    }

    IEnumerator doKnockback(float duration, int count, Vector3 explosionPos, Collider[] colliders)
    {
        int currentCount = 0;
        while(currentCount < count)
        {
            foreach (Collider hit in colliders)
            {
                if(hit.tag == "Enemy")
                {
                    // Enemy = hit.GetComponent<Enemy>();
                    Rigidbody rb =  hit.GetComponent<EnemyAIContext>().rb;
                    NavMeshAgent agent = hit.GetComponent<EnemyAIContext>().agent;
                    rb.isKinematic = false;
                    agent.isStopped = true;
                    // hit.GetComponent<EnemyAIContext>().rb;
                    Debug.Log(hit);
                    rb.AddExplosionForce(power, explosionPos, radius, 1.0F, ForceMode.Impulse);
                    StartCoroutine(resetForce(duration, rb, agent));
                    yield return new WaitForSeconds(duration);
                }
            }
            // yield return new WaitForSeconds(duration);
            currentCount++;
        }
    }
    IEnumerator resetForce(float duration, Rigidbody rb, NavMeshAgent agent)
    {
        yield return new WaitForSeconds((float)(duration-.1));
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        agent.isStopped = false;
        rb.isKinematic = true;    
    }
}
