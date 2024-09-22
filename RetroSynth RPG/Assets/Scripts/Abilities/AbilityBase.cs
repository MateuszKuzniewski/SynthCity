using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : ScriptableObject
{
    public uint ID;
    public string abilityName;
    public Sprite icon;
    public uint cooldown;
    public bool weaponAbility;

    [TextArea(15, 15)]
    public string abilityDescription;

    protected bool isUsed;
    protected bool isOnCooldown;

    private float currentCooldown;
    public virtual void Use() { }

    public void StartCooldown()
    {
        isOnCooldown = true;
        currentCooldown = (int)cooldown;
    }

    public float UpdateCooldown() 
    {
       
        if (isOnCooldown)
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
            else
            {
                isOnCooldown = false;
            }
        }
        
        return Mathf.FloorToInt(currentCooldown);
    }

    public bool IsWeaponAbility()
    {
        if (weaponAbility)
            return true;

        return false;
    }

    public bool IsOnCooldown()
    {
        if (isOnCooldown)
            return true;

        return false;
    }

    public void ResetCooldown()
    {
        isOnCooldown = false;
    }
}
