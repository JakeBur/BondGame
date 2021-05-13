using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{

    //CharacterDialogManager dialogManager => GetComponent<CharacterDialogManager>();
    DialogueManager dialogueManager => GetComponent<DialogueManager>();

    bool entered = false;

    private void OnTriggerEnter(Collider other) 
    {
       if(other.tag == "Player" && !entered)
       {
            //PersistentData.Instance.Player.GetComponent<PlayerController>().characterDialogManager = dialogManager;
            other.GetComponent<PlayerController>().dialogueManager = dialogueManager;
            dialogueManager.StartDialogue();
            entered = true;

            //set player in standby
            other.GetComponent<PlayerController>().SetStandbyState(true);
       }

      
    }

}
