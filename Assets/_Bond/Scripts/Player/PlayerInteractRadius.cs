using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractRadius : MonoBehaviour
{
    public PlayerController pc;

    private void OnTriggerEnter(Collider other) 
    {
        // Debug.Log(other.transform.tag);
        
        if(other.transform.tag == "Interactable")
        {
            pc.interactableObjects.Add(other.gameObject, other.gameObject.GetComponent<InteractableBase>());
            if( other.gameObject.GetComponent<InteractableBase>().showUI)
            {
                PersistentData.Instance.hudManager.showInteractPrompt();
            }
        }
        if(other.transform.tag == "Relic")
        {
            pc.interactableObjects.Add(other.gameObject, other.gameObject.GetComponent<InteractableBase>());
            foreach(KeyValuePair<GameObject, InteractableBase> interactObj in pc.interactableObjects)
            {
                if(interactObj.Key.transform.tag == "Relic")
                {
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(interactObj.Key.GetComponent<RelicInteractable>().relicStats,
                                                                                            interactObj.Key.GetComponent<RelicInteractable>().cost);
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                    break;
                }                
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Interactable")
        {
            pc.interactableObjects.Remove(other.gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.hideIntereactPrompt();
            }
            
            if(other.gameObject.layer == 13)
            {
                PersistentData.Instance.hudManager.HideCharacterDialogue();
                pc.dialogueManager = null;
                pc.inCharacterDialog = false;
            }
        }
        if(other.transform.tag == "Relic")
        {
            pc.interactableObjects.Remove(other.gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.hideIntereactPrompt();
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().hideUI();
            }
            else
            {
                foreach(KeyValuePair<GameObject, InteractableBase> interactObj in pc.interactableObjects)
                {
                    if(interactObj.Key.transform.tag == "Relic")
                    {
                        PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(interactObj.Key.GetComponent<RelicInteractable>().relicStats,
                                                                                                interactObj.Key.GetComponent<RelicInteractable>().cost);
                        PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                        break;
                    }   
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().hideUI();             
                }
            }
        }
    }
}
