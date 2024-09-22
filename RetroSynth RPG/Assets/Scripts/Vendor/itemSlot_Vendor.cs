
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class itemSlot_Vendor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI priceBuyNumber;
    public Item item;
    private Image displayImage;


    public void updateInfo()
    {
        displayImage = transform.Find("Image").GetComponent<Image>();


        if (item)
        {
            displayImage.sprite = item.icon;
            displayImage.color = Color.white;
        }
        else
        {
            displayImage.sprite = null;
            displayImage.color = Color.clear;

        }
        priceBuyNumber.text = item.buyPrice.ToString();
    }

    public void Buy()
    {
        if (item && !Inventory.instance.isInventoryFull() && Player.instance.GetMoney() >= item.buyPrice)
        {
            Player.instance.RemoveMoney((int)item.buyPrice);
            Inventory.instance.AddItem(item);
            UI.instance.UpdateStatPanel();
            FloatingText.DisplayFloatingText("Item purchased!");
        }
        else if(Player.instance.GetMoney() < item.buyPrice)
            FloatingText.DisplayFloatingText("Not enough credits");
        else if (Inventory.instance.isInventoryFull())
            FloatingText.DisplayFloatingText("Inventory is Full");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item)
            Tooltip.ShowToolTip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.CloseToolTip();
    }

}
