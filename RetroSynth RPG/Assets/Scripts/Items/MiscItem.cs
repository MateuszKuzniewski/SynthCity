using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Miscellaneous", menuName = "Items/Miscellaneous")]


public class MiscItem : Item
{

    public override void Use()
    {
        if (Player.instance.playerState == PlayerStates.SELLING)
        {
            Sell();
        }
    }
}
