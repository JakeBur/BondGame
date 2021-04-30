using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class ProjectileScript : MonoBehaviour
{
    [Header("Gameplay Data")]
    public float damage = 50;

    [Header("Animation Data")]
    public AnimationCurve momentumCurve;
    public float arcDistance;
    public float flyTime;
    public GameObject squibPrefab;
    
    private Vector3 toTarget;
    private Vector3 toOffset;
    private Vector3 initialPosition;

    [Header("References")]
    public GameObject trail;

    private GameObject target;
    private Rigidbody rigidBody;

    private float impactTime;
    private float startTime;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
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

        toTarget = target.transform.position - initialPosition;

        float normalizedTime = (Time.time - startTime) / (impactTime - startTime);
        float blend = momentumCurve.Evaluate(normalizedTime);
        transform.position = initialPosition + (((toTarget * blend) + (toOffset * (1-blend))) * normalizedTime);

        if (Time.time > impactTime)
        {
            target.GetComponent<EnemyAIContext>().statManager.TakeDamage(damage, ModiferType.RANGED_RESISTANCE);
            Instantiate(squibPrefab, target.transform.position, Quaternion.identity);
            SFXPlayer.PlayOneShot(SFX.LeafProjectileHitSFX, transform.position);
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

    public void SetTarget(GameObject _target, float _speed, float _damage)
    {
        target = _target;
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        damage = _damage;

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
