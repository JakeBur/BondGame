using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animation))]
public class SwordSlashVFX : MonoBehaviour
{
    public GameObject rootObject;

    private void Update()
    {
        if(!GetComponent<Animation>().isPlaying)
        {
            if ( rootObject != null )
            {
                Destroy( rootObject );
            }
            else
            {
                Destroy(transform.parent.parent.gameObject);
            }
        }
    }

}
