using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGSceneLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        
        // Debug.Log("entered : " + other.transform.name);
        if(other.transform.tag == "Player")
        {
            if(PersistentData.Instance.currRunLevel < 3)
            {
                Debug.Log("try load");
                PersistentData.Instance.LoadScene(2);
                PersistentData.Instance.currRunLevel++;
            } else {
                PersistentData.Instance.LoadScene(1);
            }
            
        } 
    }
}
