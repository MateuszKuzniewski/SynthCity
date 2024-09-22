using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public List<AbilityBase> abilityList = new List<AbilityBase>();

    public GameObject abilityPanel;

    public AbilitySlot WeaponAbilitySlot;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void updateAbilitySlots()
    {

        int index = 0;
        foreach (Transform child in abilityPanel.transform)
        {
            AbilitySlot slot = child.GetComponent<AbilitySlot>();

            if (index < abilityList.Count)
            {
                slot.ability = abilityList[index];
            }
            else
            {
                slot.ability = null;
            }
            slot.updateInfo();
            index++;
        }
    }
    private void updateWeaponAbilitySlot()
    {
        WeaponAbilitySlot.updateInfo();
    }

    public void LearnAbility(AbilityBase ab)
    {
        ab.ResetCooldown();
        if(ab.IsWeaponAbility())
        {
            WeaponAbilitySlot.ability = ab;
            updateWeaponAbilitySlot();
        }
        else
        {
            abilityList.Add(ab);
            updateAbilitySlots();
        }
    }
    
    public void UnlearnAbility(AbilityBase ab)
    {
        if (ab.IsWeaponAbility())
        {
            WeaponAbilitySlot.ability = null;
            updateWeaponAbilitySlot();
        }
        else
        {
            abilityList.Remove(ab);
            updateAbilitySlots();
        }
    }
}
