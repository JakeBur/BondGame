//Author : Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public Vector3 offset;
    public Transform cameraTarget;
    public GameObject cameraObject;

    public float smoothSpeed = 0.125f; 

    public float cameraHeightOffset = 0f;

    private float cameraDistance = -70;
    private float desiredCameraDistance = -70;

    // Distance of cameraObject to cameraTarget
    // Negative values will put cameraTarget in frame
    // Larger negative values will zoom out
    // Smaller negative values will zoom in
    [Header("Camera Distances")]
    public float farmCameraDistance = -70;
    public float exploreCameraDistance = -100;
    public float combatCameraDistance = -70;

    private float zoomSpeed = 1;
    private bool inZoom = false;

    // Controls how fast cameraDistance is lerped
    // Constrained from 0 to .999
    // Larger values are a slower lerp
    // Smaller values are a faster lerp
    [Header("Zoom Speed")]
    [Range(0f, .999f)]
    public float farmZoomSpeed = .99f;
    [Range(0f, .999f)]
    public float exploreZoomSpeed = .99f;
    [Range(0f, .999f)]
    public float combatZoomSpeed = .99f;

    void Start()
    {
        cameraTarget = PersistentData.Instance.Player.transform;
        transform.position = cameraTarget.position + offset;

        farmZoomSpeed = Mathf.Clamp( farmZoomSpeed, 0, .999f );
        exploreZoomSpeed = Mathf.Clamp( exploreZoomSpeed, 0, .999f );
        combatZoomSpeed = Mathf.Clamp( combatZoomSpeed, 0, .999f );
        
        ResetCameraHeightOffset();

        SetCameraDistanceForScene();
    }

    void FixedUpdate ()
	{
		Vector3 desiredPosition = cameraTarget.position + offset;
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

    // Resets the camera height offset
    // Camera Height offset is an offset in the Y axis
    // Just in case some transform is introduced
    public void ResetCameraHeightOffset()
    {
        Vector3 localPos = transform.localPosition;
        localPos.y = cameraHeightOffset;
        transform.localPosition = localPos;
    }

    // Sets target the camera is looking at
    // if instant is false, then it lerps to it
    // if instant is true, then it instantly sets to it
    public void SetCameraTarget( GameObject target, bool instant = false )
    {
        cameraTarget = target.transform;
        if( instant )
        {
            transform.position = cameraTarget.position + offset;
        }
    }

    // Sets target the camera is looking at
    // if instant is false, then it lerps to it
    // if instant is true, then it instantly sets to it
    public void SetCameraTarget( Transform target, bool instant = false )
    {
        cameraTarget = target;
        if( instant )
        {
            transform.position = cameraTarget.position + offset;
        }
    }

    // Automatically detect what camera distance to use
    // Based on what scene is currently loaded
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

    // Set the camera distance and zoom to farm parameters
    // if instant is false, then it lerps to it
    // if instant is true, then it instantly sets to it
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

    // Set the camera distance and zoom to explore parameters
    // if instant is false, then it lerps to it
    // if instant is true, then it instantly sets to it
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

    // Set the camera distance and zoom to combat parameters
    // if instant is false, then it lerps to it
    // if instant is true, then it instantly sets to it
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

    // Allow CameraManager to begin lerping zoom levels
    private void AdjustZoom()
    {
        inZoom = true;
    }

    // set the cameraObject's location to cameraDistance
    private void SetCameraDistance()
    {
        Vector3 localPos = cameraObject.transform.localPosition;
        localPos.z = cameraDistance;
        cameraObject.transform.localPosition = localPos;
    }
}
