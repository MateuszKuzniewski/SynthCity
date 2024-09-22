using UnityEngine;


[CreateAssetMenu(fileName = "new Clothing", menuName = "Items/Clothing")]

public class Clothing : Item
{
    [Header("Item Specific")]

    public ClothingType itemType = new ClothingType();
    public uint strength, dexterity, stamina, intelligence, tech, diplomacy;

    public override void Use()
    {
        if (Player.instance.playerState == PlayerStates.SELLING)
            Sell();
        else
            Inventory.instance.EquipItem(this);
    }

    public override void Remove()
    {
        Inventory.instance.MoveToInventory(this);
    }

    public override void AddStatistics() 
    {
        CharacterAttributes.AddModifier(0, strength);
        CharacterAttributes.AddModifier(1, dexterity);
        CharacterAttributes.AddModifier(2, stamina);
        CharacterAttributes.AddModifier(3, intelligence);
        CharacterAttributes.AddModifier(4, tech);
        CharacterAttributes.AddModifier(5, diplomacy);
        UI.instance.UpdateAttributes();
    }
    public override void RemoveStatistics() 
    {
        CharacterAttributes.RemoveModifier(0, strength);
        CharacterAttributes.RemoveModifier(1, dexterity);
        CharacterAttributes.RemoveModifier(2, stamina);
        CharacterAttributes.RemoveModifier(3, intelligence);
        CharacterAttributes.RemoveModifier(4, tech);
        CharacterAttributes.RemoveModifier(5, diplomacy);
        UI.instance.UpdateAttributes();
    }


    public override int GetItemType()
    {
        switch(itemType)
        {
            case ClothingType.Head:     return 1;
            case ClothingType.Chest:    return 2;
            case ClothingType.Legs:     return 3;
            case ClothingType.Feet:     return 4;
            case ClothingType.Glove:    return 5;       
        }
        Debug.LogError("No item type selected");
        return 0;
    }
}
public enum ClothingType
{
    Head, Chest, Legs, Feet, Glove
};


