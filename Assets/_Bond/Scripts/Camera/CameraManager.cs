//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public Vector3 offset;
    public Transform toFollow;
    public GameObject cameraObject;
    private Transform camera;

    public float smoothSpeed = 0.125f; 

    public float cameraHeightOffset = 0f;

    private float cameraDistance = -70;
    private float desiredCameraDistance = -70;

    [Header("Camera Distances")]
    public float farmCameraDistance = -70;
    public float exploreCameraDistance = -100;
    public float combatCameraDistance = -70;

    private float zoomSpeed = 1;

    [Header("Zoom Speed")]
    public float farmZoomSpeed = .01f;
    public float exploreZoomSpeed = .01f;
    public float combatZoomSpeed = .1f;

    void Start()
    {
        toFollow = PersistentData.Instance.Player.transform;

        camera = cameraObject.GetComponent<Transform>();
        
        ResetCameraHeightOffset();

        SetCameraDistanceForScene();
    }

    void FixedUpdate ()
	{
		Vector3 desiredPosition = toFollow.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        smoothedPosition.y = Mathf.Round(smoothedPosition.y);
		transform.position = smoothedPosition;
	}

    public void ResetCameraHeightOffset()
    {
        Vector3 localPos = transform.localPosition;
        localPos.y = cameraHeightOffset;
        transform.localPosition = localPos;
    }

    public void SetCameraDistance()
    {
        Vector3 localPos = camera.localPosition;
        localPos.z = cameraDistance;
        camera.localPosition = localPos;
    }

    public void SetCameraDistanceForScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if ( sceneName == "Farm" )
        {
            SetFarmCameraDistance();
        }
        else
        {
            SetExploreCameraDistance();
        }
    }

    public void SetFarmCameraDistance()
    {
        desiredCameraDistance = farmCameraDistance;
        zoomSpeed = farmZoomSpeed;

        StopAllCoroutines();
        StartCoroutine("AdjustZoom");
    }

    public void SetExploreCameraDistance()
    {
        desiredCameraDistance = exploreCameraDistance;
        zoomSpeed = exploreZoomSpeed;

        StopAllCoroutines();
        StartCoroutine("AdjustZoom");
    }

    public void SetCombatCameraDistance()
    {
        desiredCameraDistance = combatCameraDistance;
        zoomSpeed = combatZoomSpeed;

        StopAllCoroutines();
        StartCoroutine("AdjustZoom");
    }

    private IEnumerator AdjustZoom()
    {
        while( Mathf.Abs( cameraDistance - desiredCameraDistance ) > .1 )
        {
            Debug.Log(cameraDistance);
            cameraDistance = Mathf.Lerp( cameraDistance, desiredCameraDistance, zoomSpeed * Time.fixedDeltaTime );
            SetCameraDistance();
        }
        
        yield return null;
    }
}
