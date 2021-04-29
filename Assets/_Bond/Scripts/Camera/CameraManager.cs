//Author : Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public Vector3 offset;
    public Transform toFollow;
    public GameObject cameraObject;

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
    public float farmZoomSpeed = .99f;
    public float exploreZoomSpeed = .99f;
    public float combatZoomSpeed = .99f;

    void Start()
    {
        toFollow = PersistentData.Instance.Player.transform;
        
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
        Vector3 localPos = cameraObject.transform.localPosition;
        localPos.z = cameraDistance;
        cameraObject.transform.localPosition = localPos;
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

        AdjustZoom();
    }

    public void SetExploreCameraDistance()
    {
        desiredCameraDistance = exploreCameraDistance;
        zoomSpeed = exploreZoomSpeed;

        AdjustZoom();
    }

    public void SetCombatCameraDistance()
    {
        desiredCameraDistance = combatCameraDistance;
        zoomSpeed = combatZoomSpeed;

        AdjustZoom();
    }

    private void AdjustZoom()
    {
        StopAllCoroutines();
        StartCoroutine("AdjustZoomCo");
    }

    private IEnumerator AdjustZoomCo()
    {
        while( Mathf.Abs( cameraDistance - desiredCameraDistance ) > .1 )
        {
            //cameraDistance = BondMath.Approach( cameraDistance, desiredCameraDistance, zoomSpeed, Time.deltaTime);
            cameraDistance = Mathf.Lerp( cameraDistance, desiredCameraDistance, zoomSpeed * Time.deltaTime );
            SetCameraDistance();
        }
        
        yield return null;
    }
}
