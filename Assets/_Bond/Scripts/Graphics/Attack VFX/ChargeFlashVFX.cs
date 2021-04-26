using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeFlashVFX : MonoBehaviour
{
    private bool init;

    // Start is called before the first frame update
    void Start()
    {
        init = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!init)
        {
            init = true;
            transform.LookAt(Camera.main.transform.position);
        }
    }
}
