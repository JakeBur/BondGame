using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "PaletteLibrary", menuName = "ScriptableObjects/CreaturePaletteLibrary")]
public class CreaturePaletteLibrary : ScriptableObject
{
    public List<CreaturePalette> creaturePalettes;

    private List<CreaturePalette> _randomOrder;

    private void Awake() 
    {
        _randomOrder = new List<CreaturePalette>();
    }

    public CreaturePalette GetRandomPalette()
    {
        if(_randomOrder.Count == 0)
        {
            foreach(CreaturePalette palette in creaturePalettes)
            {
                _randomOrder.Add(palette);
            }
            _randomOrder = _randomOrder.OrderBy(x => Random.value).ToList();
        }

        CreaturePalette returnedPalette = _randomOrder[0];
        _randomOrder.RemoveAt(0);
        return returnedPalette;
    }
}
