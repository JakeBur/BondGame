// Eugene
// Herman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownSystem : MonoBehaviour
{
    private readonly List<CooldownData> cooldowns = new List<CooldownData>();

    private void Update() => ProcessCooldowns();

    // Returns the ability object provided the id
    public CooldownData GetCooldownByID( int id )
    {
        foreach(CooldownData cooldown in cooldowns)
        {
            if(cooldown.Id == id)
            {
                return cooldown;
            }
        }
        return null;
    }

    // Adds a new ability to be on cooldown
    public void PutOnCooldown(HasCooldown cooldown)
    {
        cooldowns.Add(new CooldownData(cooldown));
    }

    // Decrements all abilities on cooldown
    // If the ability reaches 0, then it is removed from the list
    // This function is called in Update()
    private void ProcessCooldowns()
    {
        float deltaTime = Time.deltaTime;
        
        for (int i = cooldowns.Count - 1; i >= 0; i--)
        {
            if(cooldowns[i].DecrementCooldown(deltaTime))
            {
                cooldowns.RemoveAt(i);
            }
        }
    }

    // If the ability is in the list, then it is on cooldown
    // returns true if the ability is on cooldown
    // false if not
    public bool IsOnCooldown(int id)
    {
        CooldownData cooldown = GetCooldownByID( id );

        if( cooldown != null )
        {
            return true;
        }

        return false;
    }
    
    // Finds the ability and returns its RemainingTime
    // returns 0 if the ability does not exist
    public float GetRemainingDuration(int id)
    {
        CooldownData cooldown = GetCooldownByID( id );

        if( cooldown != null )
        {
            return cooldown.RemainingTime;
        }

        return 0;
    }

    // Finds the ability and returns its TotalDuration
    // returns 0 if the ability does not exist
    public float GetTotalDuration(int id)
    {
        CooldownData cooldown = GetCooldownByID( id );

        if( cooldown != null)
        {
            return cooldown.TotalDuration;
        }
        
        return 0;
    }
}

// Actual data to be stored on the list
// Contains the id, remainingtime, and totalduration
public class CooldownData
{
    // Constructor
    public CooldownData(HasCooldown cooldown)
    {
        Id = cooldown.Id;
        RemainingTime = cooldown.CooldownDuration;
        TotalDuration = cooldown.CooldownDuration;
    }
    public int Id { get; }

    public float RemainingTime { get; private set; }
    public float TotalDuration { get; private set; }

    // Decrements RemainingTime
    // If RemainingTime reaches 0, returns true
    public bool DecrementCooldown(float deltaTime)
    {
        RemainingTime = Mathf.Max(RemainingTime- deltaTime, 0f);
        return RemainingTime == 0f;
    }
}