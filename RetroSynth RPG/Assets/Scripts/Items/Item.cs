using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("General")]
    public uint ID; 
    public string itemName;
    public Sprite icon;
    public uint buyPrice;
    public uint sellPrice;

    [TextArea(15,15)]
    public string itemDescription;

    public virtual void Use() { }
    public virtual void Remove() { }
    public virtual void AddStatistics() { }
    public virtual void RemoveStatistics() { }

    public void Sell()
    {
        Inventory.instance.RemoveItem(this);
        Player.instance.AddMoney((int)sellPrice);
        UI.instance.UpdateStatPanel();
        FloatingText.DisplayFloatingText("Item Sold");
    }

    public virtual int GetItemType()
    {
        return 69;
    }
}
