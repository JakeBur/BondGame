using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildNoticeTrigger : MonoBehaviour
{

    public GameObject creature;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player") 
        {
            if(!creature.GetComponent<CreatureAIContext>().isNoticed)
            {
                creature.GetComponent<CreatureAIContext>().isNoticed = true;
            }
        } else if(other.transform.tag == "POITree" || other.transform.tag == "POIFlower" || other.transform.tag == "POIOther")
        {
            creature.GetComponent<CreatureAIContext>().possiblePOIs.Add(other.gameObject);
        } 
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Player") 
        {
            if(creature.GetComponent<CreatureAIContext>().isNoticed)
            creature.GetComponent<CreatureAIContext>().isNoticed = false;
            creature.GetComponent<CreatureAIContext>().hasReacted = false;
        } else if(other.transform.tag == "POITree" || other.transform.tag == "POIFlower" || other.transform.tag == "POIOther") 
        {
           creature.GetComponent<CreatureAIContext>().possiblePOIs.Remove(other.gameObject);
        } 
    }
}
