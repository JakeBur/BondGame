using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ProjectileScript : MonoBehaviour
{
    GameObject target;
    Rigidbody rigidBody;

    public AnimationCurve momentumCurve;
    public float arcDistance;
    // Start is called before the first frame update
    float speed = 10;
    public float damage = 50;
    public bool isHoming = false;
    public float flyTime;

    private float momentum;

    private float impactTime;
    private float startTime;

    private Vector3 toTarget;
    private Vector3 toOffset;
    private Vector3 initialPosition;

    public GameObject trail;

    void Start()
    {
        
    }

    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
        /////////////////////////////////////////////Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            End();
            return;
        }

        if(isHoming)
        {
            toTarget = target.transform.position - initialPosition;
        }

        float normalizedTime = (Time.time - startTime) / (impactTime - startTime);
        float blend = momentumCurve.Evaluate(normalizedTime);
        transform.position = initialPosition + (((toTarget * blend) + (toOffset * (1-blend))) * normalizedTime);

        if (Time.time > impactTime)
        {
            target.GetComponent<EnemyAIContext>().statManager.TakeDamage(damage, ModiferType.RANGED_RESISTANCE);
            End();
        }
    }

    private void OnDestroy()
    {
        
    }

    private void End()
    {
        trail.transform.parent = null;
        trail.AddComponent<DestroyOnDelay>().Apply(2f);
        Destroy(gameObject);
    }

    public void SetTarget(GameObject _target, float _speed, float _damage, bool _isHoming)
    {
        target = _target;
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        speed = _speed;
        damage = _damage;
        isHoming = _isHoming;

        //Time = 1/speed

        // add initial speed at a random offset from angle to enemy (only randomize when homing)
        toTarget = target.transform.position - transform.position;
        toOffset = Quaternion.Euler(0, Mathf.Sign(Random.Range(-1, 1)) * Random.Range(60, 110), 0) * toTarget;
        toOffset = toOffset.normalized;
        toOffset -= toOffset.y * Vector3.up;
        toOffset = toOffset.normalized * arcDistance;

        impactTime = Time.time + flyTime;
        startTime = Time.time;

        initialPosition = transform.position;
    }
}
