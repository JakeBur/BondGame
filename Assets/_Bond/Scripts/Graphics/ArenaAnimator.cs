using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAnimator : MonoBehaviour
{
    public float latticeFadeBreakpoint;
    public float flash;
    public float flare;
    public float scrollSpeed;

    [Header("References")]
    [SerializeField]
    private GameObject _lattice;
    [SerializeField]
    private GameObject _smoke;

    public Gradient _smokeFlashGradient;

    private ShaderFloatAnimator latticeFade;
    private ShaderFloatAnimator smokeFlare;
    private ShaderFloatAnimator smokeScrollSpeed;
    private ShaderColorAnimator smokeFlash;

    private Material _latticeMaterial;
    private Material _smokeMaterial;

    public void Start()
    {
        _latticeMaterial = new Material(_lattice.GetComponent<MeshRenderer>().sharedMaterial);
        _lattice.GetComponent<MeshRenderer>().material = _latticeMaterial;

        _smokeMaterial = new Material(_smoke.GetComponent<MeshRenderer>().sharedMaterial);
        _smoke.GetComponent<MeshRenderer>().material = _smokeMaterial;

        latticeFade = new ShaderFloatAnimator("_FadeinBreakpoint", _latticeMaterial);
        smokeFlare = new ShaderFloatAnimator("_TransparencyNoiseStrength", _smokeMaterial);
        smokeFlash = new ShaderColorAnimator("_BackgroundEndColor", _smokeMaterial);
        smokeScrollSpeed = new ShaderFloatAnimator("_ScrollSpeed", _smokeMaterial);
    }

    private void Update()
    {
        latticeFade.Value = latticeFadeBreakpoint;
        smokeFlash.Value = _smokeFlashGradient.Evaluate(flash);
        smokeFlare.Value = flare;
        smokeScrollSpeed.Value = scrollSpeed;

        /*latticeFade.UpdateMaterial(_lattice.GetComponent<MeshRenderer>().sharedMaterial);
        smokeFlare.UpdateMaterial(_smoke.GetComponent<MeshRenderer>().sharedMaterial);
        smokeFlash.UpdateMaterial(_smoke.GetComponent<MeshRenderer>().sharedMaterial);
        smokeScrollSpeed.UpdateMaterial(_smoke.GetComponent<MeshRenderer>().sharedMaterial);*/
    }

    public void PlayEncounterBegin()
    {
        GetComponent<Animator>().SetBool("Trigger", true);
        Debug.Log("Playing encounter begin fx");
    }

    public void PlayDeathAnimation()
    {
        GetComponent<Animator>().SetBool("Die", true);
    }

    public void MatchMaterialColor()
    {
        GradientColorKey[] keys = _smokeFlashGradient.colorKeys;
        keys[0].color = _smokeMaterial.GetColor("_BackgroundEndColor");

        _smokeFlashGradient.SetKeys(keys, _smokeFlashGradient.alphaKeys);

        /*Debug.Log(_smokeFlashGradient.colorKeys[0].color);
        Debug.Log(_smokeMaterial.GetColor("_BackgroundEndColor"));

        Debug.Log(_smokeFlashGradient.colorKeys[0].color);*/
    }
}
