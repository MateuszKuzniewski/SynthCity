using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Injector", menuName = "Injectors/HollowBones")]
public class HollowBones : Item
{
    public int psychosisIncrease = 0;
    public int speedIncrease = 0;
    public int damageTaken = 0;

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
            playerSpeed.AddSpeed(speedIncrease);
            Player.instance.AddPsychosis(psychosisIncrease);
            UI.instance.UpdatePlayerStatusBars();
            FloatingText.DisplayFloatingText("DNA Injected");
            Inventory.instance.RemoveItem(this);
        }
    }
}
