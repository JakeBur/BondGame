using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDelay : MonoBehaviour
{
    public bool destroyOnStart;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        if(destroyOnStart)
        {
            Apply(delay);
        }
    }

    public void Apply(float time)
    {
        Invoke("End", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void End()
    {
        Destroy(gameObject);
    }

}
