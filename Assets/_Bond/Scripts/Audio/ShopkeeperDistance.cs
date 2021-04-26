using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperDistance : MonoBehaviour
{
    private Transform player;
    private FMODUnity.StudioEventEmitter bellsEvent;
    private Vector3 shopkeeperPos;
    private Vector3 playerPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bellsEvent = GetComponent<FMODUnity.StudioEventEmitter>();
        shopkeeperPos = transform.position;
        shopkeeperPos.z = 0;
    }

    private void FixedUpdate()
    {
        playerPos = player.position;
        playerPos.z = 0;
        bellsEvent.SetParameter("Distance", Vector3.Distance(shopkeeperPos, playerPos));
    }
}
