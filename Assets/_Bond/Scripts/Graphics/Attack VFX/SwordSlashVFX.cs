﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animation))]
public class SwordSlashVFX : MonoBehaviour
{
    public GameObject offset;

    private void Update()
    {
        if(!GetComponent<Animation>().isPlaying)
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
