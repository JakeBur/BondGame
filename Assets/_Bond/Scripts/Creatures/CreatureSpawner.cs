using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public List<Personality> PowerPersonalities = new List<Personality>();
    public List<Personality> UtilityPersonalities = new List<Personality>();
    public List<Personality> DexterityPersonalities = new List<Personality>();

    public List<CreatureData> creatureTypes = new List<CreatureData>();


    [ContextMenuItem("SpawnCreature", "SpawnCreature")]
    [SerializeField]
    private float test;
    public GameObject Creature;
    public Transform spawnPoint;
    public bool frozen = false;

    private void Awake()
    {
        SpawnCreature();
    }

    public void SpawnCreature()
    {
        //pick random creature and spawn it in the world.
        int _randomCreatureNumber = Random.Range(0, creatureTypes.Count);
        // Creature = Instantiate(creatureTypes[_randomCreatureNumber].creaturePrefab, spawnPoint.position, Quaternion.identity);
        Creature = Instantiate(creatureTypes[_randomCreatureNumber].creaturePrefab, spawnPoint.position, new Quaternion(0,140,0,0));

        //randomize color
        // Creature.GetComponent<CreatureMaterialManager>().RandomizeMaterial();//This currently only works on fragaria, bricks others

        //get access to its active data, and assign its values.
        ActiveCreatureData _ActiveCreatureData =  Creature.GetComponent<ActiveCreatureData>();
        _ActiveCreatureData.Id = Creature.GetInstanceID();
        // _ActiveCreatureData.statManager.stats[ModiferType.MAX_HEALTH].baseValue = 
        //         Random.Range(creatureTypes[_randomCreatureNumber].lifeRange.x, creatureTypes[_randomCreatureNumber].lifeRange.y);
        // _ActiveCreatureData.statManager.stats[ModiferType.CREATURE_POWER].baseValue =  
        //         (int) Random.Range(creatureTypes[_randomCreatureNumber].powerRange.x, creatureTypes[_randomCreatureNumber].powerRange.y);
        // _ActiveCreatureData.statManager.stats[ModiferType.CREATURE_UTILITY].baseValue = 
        //         (int) Random.Range(creatureTypes[_randomCreatureNumber].utilityRange.x, creatureTypes[_randomCreatureNumber].utilityRange.y);
        // _ActiveCreatureData.statManager.stats[ModiferType.CREATURE_DEXTERITY].baseValue = 
        //         (int) Random.Range(creatureTypes[_randomCreatureNumber].dexterityRange.x, creatureTypes[_randomCreatureNumber].dexterityRange.y);
        
        //select random abilities;
        List<CreatureAttackBase> _copyOfAttacks = new List<CreatureAttackBase>(creatureTypes[_randomCreatureNumber].creatureAttacks); //make a copy of the list of abilities so we can edit it without losing data
        
        int _firstAttackNumber = Random.Range(0, _copyOfAttacks.Count);
        _ActiveCreatureData.abilities.Add(_copyOfAttacks[_firstAttackNumber]);
        _copyOfAttacks.RemoveAt(_firstAttackNumber);

        int _secondAttackNumber = Random.Range(0, _copyOfAttacks.Count);
        _ActiveCreatureData.abilities.Add(_copyOfAttacks[_secondAttackNumber]);
        _copyOfAttacks.RemoveAt(_secondAttackNumber);


        
        Creature.name = Creature.GetComponent<StatManager>().baseStats.statName;
        
        
        //sets the creature icons
        Creature.GetComponent<CreatureAIContext>().icon = creatureTypes[_randomCreatureNumber].creatureIcon;
        Creature.GetComponent<CreatureAIContext>().ability1Icon = _ActiveCreatureData.abilities[0].abilityIcon;
        Creature.GetComponent<CreatureAIContext>().ability2Icon =  _ActiveCreatureData.abilities[1].abilityIcon;

        //Set frozen if wanted
        if(frozen)
        {
            Creature.GetComponent<CreatureAIContext>().creatureFrozen = true;
        }
        
        //build BT
        Creature.GetComponent<CreatureAIContext>().GetActiveCreatureData();
        Creature.GetComponent<CreatureAIRework>().BuildBT();

    }

    List<Personality> choosePersonalities(ActiveCreatureData _ActiveCreatureData, CreatureData _CreatureData){
        List<Personality> finalPersonalities = new List<Personality>();
        foreach (Personality Personality in PowerPersonalities) 
        {
            if(Personality.calcChance(_ActiveCreatureData.statManager.stats[ModiferType.CREATURE_POWER].baseValue,
                                      _CreatureData.powerRange.x,
                                      _CreatureData.powerRange.y)) 
            {
                finalPersonalities.Add(Personality);
                break;
            }
        }
        foreach (Personality Personality in UtilityPersonalities) 
        {
            if(Personality.calcChance(_ActiveCreatureData.statManager.stats[ModiferType.CREATURE_UTILITY].baseValue,
                                      _CreatureData.utilityRange.x,
                                      _CreatureData.utilityRange.y)) 
            {
                finalPersonalities.Add(Personality);
                break;
            }
        }
        foreach (Personality Personality in DexterityPersonalities) 
        {
            if(Personality.calcChance(_ActiveCreatureData.statManager.stats[ModiferType.CREATURE_DEXTERITY].baseValue,
                                      _CreatureData.dexterityRange.x,
                                      _CreatureData.dexterityRange.y)) 
            {
                finalPersonalities.Add(Personality);
                break;
            }
        }
        return finalPersonalities;
    }

}


// public enum statModifierType 
// {
//     POWER, UTILITY, DEXTERITY, LIFE, BONDRATE, NONE
// }