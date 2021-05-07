using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneIndex;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        
        // Debug.Log("entered : " + other.transform.name);
        if(other.transform.tag == "Player")
        {
            Debug.Log("try load");
            SFX.PlayLevelTransitionSFX();
            PersistentData.Instance.LoadScene(sceneIndex);
        } 
    }
}
