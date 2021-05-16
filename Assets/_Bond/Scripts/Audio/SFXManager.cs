using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SFXPlayer = FMODUnity.RuntimeManager;
using SFXUtils = FMODUnity.RuntimeUtils;

public class SFXManager : MonoBehaviour
{
    //------------------
    // Player Movement
    //------------------
    [Header("Player Movement")]
    [FMODUnity.EventRef] public string PlayerWalkGrassSFX;
    [FMODUnity.EventRef] public string PlayerRollGrassInitialSFX;
    [FMODUnity.EventRef] public string PlayerRollGrassSecondarySFX;
    [FMODUnity.EventRef] public string PlayerRollWaterSFX;
    [FMODUnity.EventRef] public string PlayerWalkWaterSFX;

    //-----------------------
    // Player Attacks Sword
    //-----------------------
    [Header("Player Attacks Sword")]
    [FMODUnity.EventRef] public string PlayerSwordSwingSFX;
    [FMODUnity.EventRef] public string PlayerSwordImpactSFX;
    [FMODUnity.EventRef] public string PlayerSwordCritSFX;

    //-----------------
    // Player Damaged
    //-----------------
    [Header("Player Damaged")]
    [FMODUnity.EventRef] public string PlayerDamagedDonutSFX;

    //--------------
    // Player Misc
    //--------------
    [Header("Player Misc.")]
    [FMODUnity.EventRef] public string PlayerWhistleSFX;

    //----------------------------
    // Creature + Enemy Movement
    //----------------------------
    [Header("Creature + Enemy Movement")]
    [FMODUnity.EventRef] public string Misc3DWalkGrassSFX;
    [FMODUnity.EventRef] public string Misc3DWalkWaterSFX;

    //-----------
    // Fragaria
    //-----------
    [Header("Fragaria")]
    [FMODUnity.EventRef] public string FragariaSunbeamSFX;
    [FMODUnity.EventRef] public string FragariaSporeTossSFX;
    [FMODUnity.EventRef] public string FragariaPetalThrowWhooshSFX;
    [FMODUnity.EventRef] public string FragariaPetalThrowEndingSFX;
    [FMODUnity.EventRef] public string FragariaPetalSawSFX;

    //----------------
    // General Enemy
    //----------------
    [Header("General Enemy")]
    [FMODUnity.EventRef] public string ArenaSpawnSFX;
    [FMODUnity.EventRef] public string EnemyDeathSFX;
    [FMODUnity.EventRef] public string EnemyLeafProjectileHitSFX;
    [FMODUnity.EventRef] public string EnemyPunchHitSFX;

    //----------------------
    // Donut (Melee Enemy)
    //----------------------
    [Header("Donut (Melee Enemy)")]
    [FMODUnity.EventRef] public string DonutSpawnExtendSFX;
    [FMODUnity.EventRef] public string DonutSpawnGrabSFX;
    [FMODUnity.EventRef] public string DonutSpawnDragSFX;
    [FMODUnity.EventRef] public string DonutAttackExtendSFX;
    [FMODUnity.EventRef] public string DonutSwipeSFX;
    [FMODUnity.EventRef] public string DonutRetractSFX;

    //----------------------------
    // Shopkeeper + Currency SFX
    //----------------------------
    [Header("Shopkeeper + Currency")]
    [FMODUnity.EventRef] public string CollectMoneySFX;

    //---------
    // UI SFX
    //---------
    [Header("UI SFX")]
    [FMODUnity.EventRef] public string MenuOpenSFX;
    [FMODUnity.EventRef] public string ButtonClickSFX;
    [FMODUnity.EventRef] public string CreatureSwapSFX;
    [FMODUnity.EventRef] public string CreatureBefriendSFX;
    [FMODUnity.EventRef] public string LevelTransitionSFX;
    [FMODUnity.EventRef] public string RelicPickupSFX;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Play2DWalkSFX(Transform playerTransform)
    {
        int waterLayerMask = 1 << 4;
        int groundLayerMask = 1 << 15;
        RaycastHit hit;

        if (Physics.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, waterLayerMask | groundLayerMask))
        {
            switch(hit.collider.tag)
            {
                case "Terrain":
                    SFXPlayer.PlayOneShot(PlayerWalkGrassSFX, transform.position);
                    break;
                case "Water":
                    SFXPlayer.PlayOneShot(PlayerWalkWaterSFX, transform.position);
                    break;
                default:
                    Debug.Log("2D Invalid ground");
                    return;
            }
        }
        else
        {
            Debug.Log("2D Invalid ground");
            return;
        }
    }

    public void PlayRollInitialSFX(Transform playerTransform)
    {
        int waterLayerMask = 1 << 4;
        int groundLayerMask = 1 << 15;
        RaycastHit hit;

        if (Physics.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, waterLayerMask | groundLayerMask))
        {
            switch(hit.collider.tag)
            {
                case "Terrain":
                    SFXPlayer.PlayOneShot(PlayerRollGrassInitialSFX, transform.position);
                    break;
                case "Water":
                    SFXPlayer.PlayOneShot(PlayerRollWaterSFX, transform.position);
                    break;
                default:
                    Debug.Log("2D Invalid ground");
                    return;
            }
        }
        else
        {
            Debug.Log("2D Invalid ground");
            return;
        }
    }

    public void PlayRollSecondarySFX(Transform playerTransform)
    {
        int waterLayerMask = 1 << 4;
        int groundLayerMask = 1 << 15;
        RaycastHit hit;

        if (Physics.Raycast(playerTransform.position, playerTransform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, waterLayerMask | groundLayerMask))
        {
            switch(hit.collider.tag)
            {
                case "Terrain":
                    SFXPlayer.PlayOneShot(PlayerRollGrassSecondarySFX, transform.position);
                    break;
                case "Water":
                    break;
                default:
                    Debug.Log("2D Invalid ground");
                    return;
            }
        }
        else
        {
            Debug.Log("2D Invalid ground");
            return;
        }
    }

    public void Play3DWalkSFX(int tag, Transform charTransform)
    {
        int waterLayerMask = 1 << 4;
        int groundLayerMask = 1 << 15;
        RaycastHit hit;

        if (Physics.Raycast(charTransform.position, charTransform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, waterLayerMask | groundLayerMask))
        {
            switch(hit.collider.tag)
            {
                case "Terrain":
                    Play3DWalkOneShot(Misc3DWalkGrassSFX, tag, charTransform.position);
                    break;
                case "Water":
                    Play3DWalkOneShot(Misc3DWalkWaterSFX, tag, charTransform.position);
                    break;
                default:
                    Debug.Log("3D Invalid ground");
                    return;
            }
        }
        else
        {
            Debug.Log("3D Invalid ground");
            return;
        }
    }

    private void Play3DWalkOneShot(string eventPath, int tag, Vector3 position = new Vector3())
    {
        //--------------------------------
        // List of creature + enemy tags
        // 0 - Fragaria
        // 1 - Rabbit
        // 2 - Donut (Melee Enemy)
        //--------------------------------
        var instance = SFXPlayer.CreateInstance(eventPath);
        instance.set3DAttributes(SFXUtils.To3DAttributes(position));
        instance.setParameterByName("MoverTag", tag);
        instance.start();
        instance.release();
    }

    public void Play3DWalkGrassSFX(int tag, Vector3 position = new Vector3())
    {
        //--------------------------------
        // List of creature + enemy tags
        // 0 - Fragaria
        // 1 - Rabbit
        // 2 - Donut (Melee Enemy)
        //--------------------------------
        var instance = SFXPlayer.CreateInstance(Misc3DWalkGrassSFX);
        instance.set3DAttributes(SFXUtils.To3DAttributes(position));
        instance.setParameterByName("MoverTag", tag);
        instance.start();
        instance.release();
    }

    public void PlayFragariaPetalThrowWhooshSFX(int tag, Vector3 position = new Vector3())
    {
        var instance = SFXPlayer.CreateInstance(FragariaPetalThrowWhooshSFX);
        instance.set3DAttributes(SFXUtils.To3DAttributes(position));
        instance.setParameterByName("Count", tag);
        instance.start();
        instance.release();
    }

    public void PlayCreatureBefriendSFX(string creatureType, Vector3 position = new Vector3())
    {
        int creatureTag;
        switch(creatureType)
        {
            case "Fragaria":
                creatureTag = 0;
                break;
            case "Aquaphim":
                creatureTag = 1;
                break;
            case "Lilibun":
                creatureTag = 2;
                break;
            case "Slugger":
                creatureTag = 3;
                break;
            default:
                Debug.Log("Invalid creature type! " + creatureType);
                return;
        }

        var instance = SFXPlayer.CreateInstance(CreatureBefriendSFX);
        instance.set3DAttributes(SFXUtils.To3DAttributes(position));
        instance.setParameterByName("Creature Tag", creatureTag);
        instance.start();
        instance.release();
    }

    public void PlayLevelTransitionSFX()
    {
        SFXPlayer.PlayOneShot(LevelTransitionSFX, transform.position);
    }
}
