using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureMaterialManager : MonoBehaviour
{
    public CreaturePaletteLibrary paletteLibrary;
    public List<GameObject> subModels;

    private void Start() {
        RandomizeMaterial();
    }

    public void RandomizeMaterial()
    {
        ApplyMaterial(paletteLibrary.GetRandomPalette().material);
    }

    public void ApplyMaterial(Material material)
    {
        subModels.ForEach(model => model.GetComponent<SkinnedMeshRenderer>().sharedMaterial = material);
    }
}
