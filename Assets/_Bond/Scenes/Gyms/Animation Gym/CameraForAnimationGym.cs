using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForAnimationGym : MonoBehaviour
{
    private CameraManager cameraManager => gameObject.GetComponent<CameraManager>();
    public GameObject target;

    void Start()
    {
        StartCoroutine("StartCamera");
    }

    private IEnumerator StartCamera()
    {
        yield return new WaitForSeconds(1);
        SetCameraParameters();
    }

    private void SetCameraParameters()
    {
        cameraManager.SetCameraTarget(target);
        cameraManager.SetManualCameraDistance( -25f, 1f, true );
    }
}
