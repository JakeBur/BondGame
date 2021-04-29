using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class UiFunctions : MonoBehaviour
{
    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX;

    private void Start()
    {
        if (SFX == null)
        {
            try
            {
                SFX = PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
            }
            catch
            {
                SFX = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
            }
        }
    }

    public void PlayClickSFX()
    {
        SFXPlayer.PlayOneShot(SFX.ButtonClickSFX, transform.position);
    }

    public void TransitionScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneNoPersist(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetTimeScale(int index)
    {
        Time.timeScale = index;
    }

    public void ExitGame()
    {
         Application.Quit();
    }
}
