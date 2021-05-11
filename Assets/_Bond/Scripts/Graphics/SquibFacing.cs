using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquibFacing : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PersistentData.Instance.Player.transform);
        transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
    }
}
