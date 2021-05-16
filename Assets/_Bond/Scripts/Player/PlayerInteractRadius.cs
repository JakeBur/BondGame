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
            if(other.transform.GetComponentInParent<CreatureAIContext>())
            {
                if(!other.transform.GetComponentInParent<CreatureAIContext>().creatureFrozen)
                {
                    if( other.gameObject.GetComponent<InteractableBase>().showUI)
                    {
                        PersistentData.Instance.hudManager.ShowInteractPrompt();
                    }
                }
            } else
            {
                if( other.gameObject.GetComponent<InteractableBase>().showUI)
                {
                    PersistentData.Instance.hudManager.ShowInteractPrompt();
                }
            }
        }
        if(other.transform.tag == "Potion")
        {
            pc.interactableObjects.Add(other.gameObject, other.gameObject.GetComponent<InteractableBase>());
            foreach(KeyValuePair<GameObject, InteractableBase> interactObj in pc.interactableObjects)
            {
                if(interactObj.Key.transform.tag == "Potion")
                {
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(interactObj.Key.GetComponent<PotionInteractable>().relicStats,
                                                                                            interactObj.Key.GetComponent<PotionInteractable>().cost);
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                    if( other.gameObject.GetComponent<InteractableBase>().showUI)
                    {
                        PersistentData.Instance.hudManager.ShowInteractPrompt();
                    }
                    break;
                }                
            }
        }
        if(other.transform.tag == "AcornBag")
        {
            pc.interactableObjects.Add(other.gameObject, other.gameObject.GetComponent<InteractableBase>());
            foreach(KeyValuePair<GameObject, InteractableBase> interactObj in pc.interactableObjects)
            {
                if(interactObj.Key.transform.tag == "AcornBag")
                {
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(interactObj.Key.GetComponent<AcornBagInteractable>().relicStats,
                                                                                            interactObj.Key.GetComponent<AcornBagInteractable>().cost);
                    PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                    if( other.gameObject.GetComponent<InteractableBase>().showUI)
                    {
                        PersistentData.Instance.hudManager.ShowInteractPrompt();
                    }
                    break;
                }                
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
                    if( other.gameObject.GetComponent<InteractableBase>().showUI)
                    {
                        PersistentData.Instance.hudManager.ShowInteractPrompt();
                    }
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
                PersistentData.Instance.hudManager.HideIntereactPrompt();
            }
            
            if(other.gameObject.layer == 13)
            {
                PersistentData.Instance.hudManager.HideCharacterDialogue();
                pc.dialogueManager = null;
                pc.inCharacterDialog = false;
            }
        }
        if(other.transform.tag == "Potion")
        {
            pc.interactableObjects.Remove(other.gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.HideIntereactPrompt();
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
        if(other.transform.tag == "AcornBag")
        {
            pc.interactableObjects.Remove(other.gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.HideIntereactPrompt();
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
        if(other.transform.tag == "Relic")
        {
            pc.interactableObjects.Remove(other.gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.HideIntereactPrompt();
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
