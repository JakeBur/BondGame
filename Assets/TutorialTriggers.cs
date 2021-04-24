using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{

    CharacterDialogManager dialogManager => GetComponent<CharacterDialogManager>();

    bool entered = false;

    private void OnTriggerEnter(Collider other) 
    {
       if(other.tag == "Player" && !entered)
       {
           PersistentData.Instance.Player.GetComponent<PlayerController>().characterDialogManager = dialogManager;
           dialogManager.StartConvo();
           entered = true;

            //set player in standby
            PersistentData.Instance.Player.GetComponent<PlayerController>().SetStandbyState(true);
       }

      
    }
}
