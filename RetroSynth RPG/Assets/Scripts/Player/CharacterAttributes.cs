using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    
    public uint strength, dexterity, stamina, intelligence, tech, diplomacy, loyalty;

    private readonly List<uint> AttributesList = new List<uint>();
    public static CharacterAttributes instance;

    // Start is called before the first frame update
   void Start()
    {
        if(instance == null)
            instance = this;

        AttributesList.Add(strength);
        AttributesList.Add(dexterity);
        AttributesList.Add(stamina);
        AttributesList.Add(intelligence);
        AttributesList.Add(tech);
        AttributesList.Add(diplomacy);
        AttributesList.Add(loyalty);
        UI.instance.UpdateAttributes();
    }

    public static void AddModifier(int statistic, uint amount)
    {
        instance.AttributesList[statistic] += amount;
    }

    public static void RemoveModifier(int statistic, uint amount)
    {
        instance.AttributesList[statistic] -= amount;
    }

    public static uint GetModifier(int statistic)
    {
        return instance.AttributesList[statistic];
    }
}
