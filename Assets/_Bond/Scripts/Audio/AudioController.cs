using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter masterMusicEvent;
    public FMODUnity.StudioEventEmitter ambientNoiseEvent;
    private float waitTime;

    private void Start()
    {
        //wait for one bar before resetting Game State
        waitTime = 4f/ 118f * 60f;
    }

    private void Update()
    {
       
    }

    public void BeginFarmMusic()
    {
        masterMusicEvent.SetParameter("Game State", 0);
    }

    public void BeginOverworldMusic()
    {
        masterMusicEvent.SetParameter("Game State", 1);
    }

    public void BeginCombatMusic()
    {
        masterMusicEvent.SetParameter("Game State", 2);
    }

    public void BeginCombatMusicOutro()
    {
        masterMusicEvent.SetParameter("Game State", 3);
        StartCoroutine(EndCombatMusicOutro());
    }

    private IEnumerator EndCombatMusicOutro()
    {
        yield return new WaitForSeconds(waitTime);
        masterMusicEvent.SetParameter("Game State", 1);
    }
}
