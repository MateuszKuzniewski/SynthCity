using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class itemSlot_Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private Image displayImage;


    private void Start()
    {      
        updateInfo();
    }

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

    public void UseItem() // use from UI
    {
        if(item)
        { 
            item.Use();
            Tooltip.UpdateToolTip(item);
        }
    }
}
