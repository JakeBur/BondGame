using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
