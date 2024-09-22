using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public GameObject vendorPanel;
    public List<Item> vendorItems = new List<Item>();

    public void UpdateVendorSlots()
    {
        int index = 0;
        foreach (Transform child in vendorPanel.transform)
        {
            itemSlot_Vendor slot = child.GetComponent<itemSlot_Vendor>();

            if (index < vendorItems.Count)
            {
                slot.item = vendorItems[index];
            }
            else
            {
                slot.item = null;
            }
            slot.updateInfo();
            index++;
        }
    }



    public void CloseVendorPanel()
    {
        vendorPanel.SetActive(false);
    }
}
