using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractRadius : MonoBehaviour
{
    public PlayerController pc;

    private void FixedUpdate() {
        if(pc.interactableObjects.Count > 0)
        {
            pc.updateInteractDistances();
            pc.displayInteractUI();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Interactable")
        {
            pc.interactableObjects.Add(other.gameObject);
            pc.updateInteractDistances();
            pc.displayInteractUI();        
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Interactable")
        {
            
            if(other.gameObject.layer == 13)
            {
                PersistentData.Instance.hudManager.HideCharacterDialogue();
                pc.dialogueManager = null;
                pc.inCharacterDialog = false;
            }

            if(other.gameObject == pc.updateInteractDistances())
            {
                if(other.gameObject.GetComponentInParent<CreatureAIContext>())
                {
                    //HIDECREATUREBEFRIENDUI;
                }
                else
                {
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().hideUI();
                }
                
            }
            PersistentData.Instance.hudManager.HideIntereactPrompt();
            pc.interactableObjects.Remove(other.gameObject);
            pc.displayInteractUI();
        }

    }
}
