using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
    [Header("Item Specific")]
    public int Speed = 0;

    public override void Use()
    {
        if (Player.instance.playerState == PlayerStates.SELLING)
        {
            Sell();
        }
        else
        {
            GameObject player = Inventory.instance.player;
            PlayerMovement playerSpeed = player.GetComponent<PlayerMovement>();

            playerSpeed.AddSpeed(Speed);
            Inventory.instance.RemoveItem(this);
        }
    }
}
