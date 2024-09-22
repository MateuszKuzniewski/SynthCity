
public class itemSlot_Equipment : itemSlot_Inventory
{
    public void Clear()
    {
        if (item && !Inventory.instance.isInventoryFull())
        {
            item.Remove();
            Tooltip.UpdateToolTip(item);
        }
        else if(item && Inventory.instance.isInventoryFull())
        {
            FloatingText.DisplayFloatingText("Inventory is full");
        }
    }
}
