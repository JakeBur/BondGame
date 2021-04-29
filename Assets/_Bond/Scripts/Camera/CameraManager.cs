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
    private bool inZoom = false;

    [Header("Zoom Speed")]
    [Range(0f, .999f)]
    public float farmZoomSpeed = .99f;
    [Range(0f, .999f)]
    public float exploreZoomSpeed = .99f;
    [Range(0f, .999f)]
    public float combatZoomSpeed = .99f;

    void Start()
    {
        toFollow = PersistentData.Instance.Player.transform;
        transform.position = toFollow.position + offset;

        farmZoomSpeed = Mathf.Clamp( farmZoomSpeed, 0, .999f );
        exploreZoomSpeed = Mathf.Clamp( exploreZoomSpeed, 0, .999f );
        combatZoomSpeed = Mathf.Clamp( combatZoomSpeed, 0, .999f );
        
        ResetCameraHeightOffset();

        SetCameraDistanceForScene();
    }

    void FixedUpdate ()
	{
		Vector3 desiredPosition = toFollow.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        smoothedPosition.y = Mathf.Round(smoothedPosition.y);
		transform.position = smoothedPosition;

        if( inZoom )
        {
            cameraDistance = BondMath.Approach( cameraDistance, desiredCameraDistance, zoomSpeed, Time.deltaTime);
            //cameraDistance = Mathf.Lerp( cameraDistance, desiredCameraDistance, zoomSpeed * Time.deltaTime );
            SetCameraDistance();

            if( Mathf.Abs( cameraDistance - desiredCameraDistance ) < .1 )
            {
                inZoom = false;
            }
        }
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
            SetFarmCameraDistance( true );
        }
        else
        {
            SetExploreCameraDistance( true );
        }
    }

    public void SetFarmCameraDistance( bool instant = false )
    {
        if( !instant )
        {
            desiredCameraDistance = farmCameraDistance;
            zoomSpeed = farmZoomSpeed;

            AdjustZoom();
        }
        else
        {
            cameraDistance = farmCameraDistance;
            zoomSpeed = farmZoomSpeed;

            SetCameraDistance();
        }
    }

    public void SetExploreCameraDistance( bool instant = false )
    {
        if( !instant )
        {
            desiredCameraDistance = exploreCameraDistance;
            zoomSpeed = exploreZoomSpeed;

            AdjustZoom();
        }
        else
        {
            cameraDistance = exploreCameraDistance;
            zoomSpeed = exploreZoomSpeed;

            SetCameraDistance();
        }
    }

    public void SetCombatCameraDistance( bool instant = false )
    {
        if( !instant )
        {
            desiredCameraDistance = combatCameraDistance;
            zoomSpeed = combatZoomSpeed;

            AdjustZoom();
        }
        else
        {
            cameraDistance = combatCameraDistance;
            zoomSpeed = combatZoomSpeed;

            SetCameraDistance();
        }
    }

    private void AdjustZoom()
    {
        inZoom = true;
        //StopAllCoroutines();
        //StartCoroutine("AdjustZoomCo");
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
