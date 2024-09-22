using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemBox : MonoBehaviour
{
    [Header("Items in the container")]
    //public List<Item> item = new List<Item>();
    public Item[] item = new Item[3];
    [Space(2)]

    [Header("Materials for the outline")]
    public Material materialDefault;
    public Material materialOutline;

    private UI ui;
    private bool boxOpen = false;
    private GameManager gameManager;
    private Renderer rend;
    
    [HideInInspector] public bool boxLooted;
    [HideInInspector] public int size;
    [HideInInspector] public int index;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        ui = GameObject.FindGameObjectWithTag("CanvasManager").GetComponent<UI>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        size = 0;

    }
    private void Update()
    {
        if (size == item.Length)
            boxLooted = true;

        if (boxLooted)         
            rend.material = materialDefault;

        if (boxOpen)
            if (Player.instance.playerState != PlayerStates.LOOTING)
                CloseLootPanel();

    }

    private void OnMouseOver()
    {
        if (!enabled)
            return;

        rend.material = materialOutline;

        if (Input.GetMouseButtonDown(0))
        {
            if (boxLooted == false)
            {
                Player.instance.playerState = PlayerStates.LOOTING;
                ui.ShowLoot(true);
                boxOpen = true;
                for (int i = 0; i < item.Length; i++)
                {

                    if (item.Length == 3)
                    {
                        ui.Item_1.gameObject.SetActive(true);
                        ui.Item_2.gameObject.SetActive(true);
                        ui.Item_3.gameObject.SetActive(true);
                    }
                    else if (item.Length == 2)
                    {
                        ui.Item_1.gameObject.SetActive(true);
                        ui.Item_2.gameObject.SetActive(true);
                        ui.Item_3.gameObject.SetActive(false);
                    }
                    else if (item.Length == 1)
                    {
                        ui.Item_1.gameObject.SetActive(true);
                        ui.Item_2.gameObject.SetActive(false);
                        ui.Item_3.gameObject.SetActive(false);
                    }


                    TextMeshProUGUI x = ui.AssignLootText(i + 1);
                    x.text = item[i].itemName;

                
                }
            }
            // do not remove
            index = gameManager.FindIndex(gameObject, gameManager.itemBoxList);

        }
    }

    private void OnMouseExit()
    {
        if (!enabled)
            return;

        rend.material = materialDefault;
    }

    public void CloseLootPanel()
    {
        ui.ShowLoot(false);
        boxOpen = false;
    }
}
