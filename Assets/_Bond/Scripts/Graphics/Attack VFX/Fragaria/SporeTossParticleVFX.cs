using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SporeTossParticleVFX : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
