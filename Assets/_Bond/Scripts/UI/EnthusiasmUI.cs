using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnthusiasmUI : MonoBehaviour
{
    private PlayerController player => PersistentData.Instance.Player.GetComponent<PlayerController>();
    public Image enthusiasm;
    public Image enthusiasmBar;

    
    private void FixedUpdate() 
    {
        enthusiasm.transform.rotation = Camera.main.transform.rotation;
    }

    public void UpdateEnthusiasm()
    {
        var creatureStats = player.currCreatureContext.creatureStats.statManager;
        enthusiasmBar.fillAmount = ((creatureStats.getStat(ModiferType.CURR_ENTHUSIASM) / creatureStats.getStat(ModiferType.MAX_ENTHUSIASM)));
    }
}
