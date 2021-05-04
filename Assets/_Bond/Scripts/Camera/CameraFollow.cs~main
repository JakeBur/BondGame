//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform toFollow;

    public float smoothSpeed = 0.125f; 

    void Start()
    {
        toFollow = PersistentData.Instance.Player.transform;
        transform.position = toFollow.position + offset;
    }

    void FixedUpdate ()
	{
		Vector3 desiredPosition = toFollow.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        smoothedPosition.y = Mathf.Round(smoothedPosition.y);
		transform.position = smoothedPosition;
	}
}
