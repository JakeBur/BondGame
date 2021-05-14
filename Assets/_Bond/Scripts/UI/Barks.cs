using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Barks : MonoBehaviour
{
    public List<string> enterBarkList = new List<string>();
    public List<string> purchaseBarkList = new List<string>();
    public List<string> exitBarkList = new List<string>();
    public TextMeshProUGUI barkBox;
    private ShopkeeperManager shopkeeperManager;
    
    private bool doBarks;
    private bool welcomeBarks;
    private bool exitBarks;
    private float timer = 0;
    private float showTimer = 10;
    private float textFadeTimer = 5;
    private int lastRelicCount = 4;

    private void Start() {
        shopkeeperManager = GetComponent<ShopkeeperManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            setBarks(0);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            setBarks(2);
        }
    }
    
    private void Update()
    {
        timer+= Time.deltaTime;
        if(welcomeBarks)
        {
            barkBox.text = enterBarkList[Random.Range(0,enterBarkList.Count)];
            welcomeBarks = false;
            timer = 0;
        } else if(shopkeeperManager.relicsForSale.Count < lastRelicCount)
        {
            barkBox.text = purchaseBarkList[Random.Range(0,purchaseBarkList.Count)];
            timer = 0;
            lastRelicCount--;
        } else if(exitBarks)
        {
            barkBox.text = exitBarkList[Random.Range(0,exitBarkList.Count)];
            exitBarks = false;
            timer = 0;
        }

        //Text fade timer
        if(timer >= textFadeTimer)
        {
            barkBox.text = "";
        }
    }

    //0 = Welcome, 1 = Purchase, 2 = Exit
    private void setBarks(int barkType)
    {
        switch(barkType)
        {
            case 0:
                welcomeBarks = true;
                exitBarks = false;
                break;
            case 1:
                welcomeBarks = false;
                exitBarks = false;
                break;
            case 2:
                exitBarks = true;
                welcomeBarks = false;
                break;
            default:
                break;
        }
    }
}
