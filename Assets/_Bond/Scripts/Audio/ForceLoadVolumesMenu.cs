using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLoadVolumesMenu : MonoBehaviour
{
    public AudioSettings settings;

    private void Start()
    {
        settings.LoadVolumesOnStart();
    }
}
