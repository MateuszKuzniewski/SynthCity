using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable", menuName = "Items/Credits")]
public class Currency : Item
{
    [Header("Item Specific")]
    public int value = 0;

    public override void Use()
    {
        Player.instance.AddMoney(value);
        UI.instance.UpdateStatPanel();
        Inventory.instance.RemoveItem(this);
    }
}
