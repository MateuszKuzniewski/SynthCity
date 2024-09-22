using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  
    public static Inventory instance;
    public List<Item> listInventory = new List<Item>();
    public Item[] listEquipment = new Item[6];
    public GameObject player;
    public GameObject inventoryPanel;
    public GameObject equipmentPanel;

    private bool isFull;
    private List<int> itemPos = new List<int>();


    private void Start()
    {
        instance = this;

        updateInventorySlots();
        updateEquipmentSlots();

    }
    private void Update()
    {
        if (listInventory.Count <= 19)
        {
            isFull = false;
        }
        else
        {
            isFull = true;
        }
    }

    private void updateInventorySlots()
    {

        int index = 0;
        foreach (Transform child in inventoryPanel.transform)
        {
            itemSlot_Inventory slot = child.GetComponent<itemSlot_Inventory>();

            if (index < listInventory.Count)
            {
                slot.item = listInventory[index];
            }
            else
            {
                slot.item = null;
            }
            slot.updateInfo();
            index++;
        }
    }

    private void updateEquipmentSlots()
    {
        int index = 0;
        foreach (Transform child in equipmentPanel.transform)
        {
            itemSlot_Equipment slot = child.GetComponent<itemSlot_Equipment>();

            if (index < listEquipment.Length)
            {
                slot.item = listEquipment[index];
            }
            else
            {
                slot.item = null;

            }
            slot.updateInfo();
            index++;
        }
    }

    private void ReplaceItemInSlot(Item item)
    {
        MoveToInventory(listEquipment[item.GetItemType() - 1]);
        EquipItem(item);
    }


    public void AddItem(Item item)
    {
        if (listInventory.Count <= 19)
        {
            listInventory.Add(item);
        }
        else
        {
            FloatingText.DisplayFloatingText("Inventory is full");
        }

        updateInventorySlots();

    }

    public void RemoveItem(Item item)
    {
        listInventory.Remove(item);
        updateInventorySlots();

    }

    public void EquipItem(Item item)
    {
        if (listEquipment[item.GetItemType() - 1] == null)
        {
            item.AddStatistics();
            switch (item.GetItemType())
            {
                case 0: break;
                case 1: listEquipment[0] = item; break;
                case 2: listEquipment[1] = item; break;
                case 3: listEquipment[2] = item; break;
                case 4: listEquipment[3] = item; break;
                case 5: listEquipment[4] = item; break;
                case 6: listEquipment[5] = item; break;
            }
  
        }
        else
        {
            ReplaceItemInSlot(item);
        }

       
        updateEquipmentSlots();
        listInventory.Remove(item);
        updateInventorySlots();
    }

    // Move equipped item to the inventory
    public void MoveToInventory(Item item)
    {
        item.RemoveStatistics();
        switch (item.GetItemType())
        {
            case 0: break;
            case 1: listEquipment[0] = null; break;
            case 2: listEquipment[1] = null; break;
            case 3: listEquipment[2] = null; break;
            case 4: listEquipment[3] = null; break;
            case 5: listEquipment[4] = null; break;
            case 6: listEquipment[5] = null; break;
        }

        updateEquipmentSlots();
        listInventory.Add(item);
        updateInventorySlots();
    }

    public uint CheckForItemAmountByID(uint id)
    {
        uint amount = 0;
        foreach (Item item in listInventory)
        {
            if (item.ID == id)
                amount++;
        }

        return amount;
    }

    public void RemoveItemByID(uint id, uint amount)
    {
            
        int x = 0;
        int a = 0;
        for(int i = 0; i < listInventory.Count; i++)
        {
            if (listInventory[i].ID == id)
            {
                x = listInventory.IndexOf(listInventory[i]);
                itemPos.Add(i);
               
            }

        }

        for (int j = 0; j < amount; j++)
        {
            listInventory.RemoveAt(itemPos[j] - a);
            a++;

        }


        itemPos.Clear();
        updateInventorySlots();
    }


    public bool isInventoryFull()
    {
        return isFull;
    }

    public Item GetEquippedWeapon()
    {
        if (listEquipment[5] != null)
            return listEquipment[5];
        else
            return null;
    }
}
