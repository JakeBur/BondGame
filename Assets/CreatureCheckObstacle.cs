using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCheckObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<PlayerController>().currCreature == null)
            {
                 Debug.Log("befriend a creature before continuing");
                 //popup text in Hud
            }
            else
            {
                //disable gameobject
                gameObject.SetActive(false);
            }
           

            //text popup

        }
    }
}
