using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class LootItem : MonoBehaviour, IPointerDownHandler
{
    public int index;

    private string itemName;
    private GameManager gameManager;


    private void Start()
    {       
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        
        // Assign index to each game object in ItemBox List
        for (int i = 0; i < gameManager.itemBoxList.Count; i++)
            index = gameManager.index;


        if (pointerEventData.button == PointerEventData.InputButton.Left && Inventory.instance.isInventoryFull() == false)
        {
            itemName = gameObject.name;
            gameObject.SetActive(false);
            gameManager.itemBoxList[index].GetComponent<ItemBox>().size++;  
        }


        // Add items to inventory when UI element clicked is corresponding to the item slot
        if (itemName == "ItemSlot_01")
        {
            Inventory.instance.AddItem(gameManager.itemBoxList[index].GetComponent<ItemBox>().item[0]);
        }
        else if (itemName == "ItemSlot_02")
        {
            Inventory.instance.AddItem(gameManager.itemBoxList[index].GetComponent<ItemBox>().item[1]);
        }
        else if (itemName == "ItemSlot_03")
        {
            Inventory.instance.AddItem(gameManager.itemBoxList[index].GetComponent<ItemBox>().item[2]);
        }
        
    }
   
}

