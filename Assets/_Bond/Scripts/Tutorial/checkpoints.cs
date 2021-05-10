using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoints : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public int num;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            tutorialManager.UpdateSpawnpoint(num);
            gameObject.SetActive(false);
        }
        
    }
}
