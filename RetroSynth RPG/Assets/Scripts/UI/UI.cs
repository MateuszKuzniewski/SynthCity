using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject AttributePanel;
    public GameObject LootPanel;
    public GameObject InventoryPanel;
    public GameObject ItemsPanel;
    public GameObject VendorPanel;
    public GameObject PlayerPortait;
    public GameObject EnemyPortait;
    [Space(2)]
    [Header("Loot Panel")]
    public GameObject Item_1;
    public GameObject Item_2;
    public GameObject Item_3;
    [Space(2)]
    [Header("Attributes")]
    public TextMeshProUGUI StrengthNum;
    public TextMeshProUGUI DexterityNum;
    public TextMeshProUGUI StaminaNum;
    public TextMeshProUGUI IntelligenceNum;
    public TextMeshProUGUI TechNum;
    public TextMeshProUGUI DiplomacyNum;
    public TextMeshProUGUI LoyaltyNum;
    [Space(2)]
    [Header("Currency")]
    public TextMeshProUGUI CurrencyNum;
    [Space(2)]
    [Header("Health/Psychosis Bars")]
    public Slider healthBar;
    public Slider psychosisBar;
    [Space(2)]
    [Header("Enemy")]
    public Slider enemyHealthBar;
    public TextMeshProUGUI enemyName;


    private bool isUIActive;
    private bool isInventoryActive;

    public static UI instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
   
        ShowLoot(false);
        EnemyPortait.SetActive(false);
    }

    public void UpdateStatPanel()
    {
        CurrencyNum.text = Player.instance.GetMoney().ToString();
    }

    public void UpdatePlayerStatusBars()
    {
        healthBar.maxValue = Player.instance.GetMaxHealth();
        psychosisBar.maxValue = Player.instance.GetMaxPsychosis();

        healthBar.value = Player.instance.GetHealth();
        psychosisBar.value = Player.instance.GetPsychosis();
    }
    public void UpdateEnemyHealthBar(GameObject enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        enemyHealthBar.maxValue = e.maxHealth;
        enemyHealthBar.value = e.health;
        enemyName.text = e.name;
    }

    public void UpdateAttributes()
    {
        StrengthNum.text =      CharacterAttributes.GetModifier(0).ToString();
        DexterityNum.text =     CharacterAttributes.GetModifier(1).ToString();
        StaminaNum.text =       CharacterAttributes.GetModifier(2).ToString();
        IntelligenceNum.text =  CharacterAttributes.GetModifier(3).ToString();
        TechNum.text =          CharacterAttributes.GetModifier(4).ToString();
        DiplomacyNum.text =     CharacterAttributes.GetModifier(5).ToString();
        LoyaltyNum.text =       CharacterAttributes.GetModifier(6).ToString();
                                                   
  
        for (int i = 0; i <= 6; i++)
        {
            if (CharacterAttributes.GetModifier(i) != 0)
            {
                switch (i)
                {
                    case 0:  StrengthNum.color = Color.green;     break;
                    case 1:  DexterityNum.color = Color.green;    break; 
                    case 2:  StaminaNum.color = Color.green;      break;
                    case 3:  IntelligenceNum.color = Color.green; break;
                    case 4:  TechNum.color = Color.green;         break;
                    case 5:  DiplomacyNum.color = Color.green;    break;
                    case 6:  LoyaltyNum.color = Color.green;      break;
                    
                }           
            }
            else
            {
                switch (i)
                {
                    case 0: StrengthNum.color = Color.white;      break;
                    case 1: DexterityNum.color = Color.white;     break;
                    case 2: StaminaNum.color = Color.white;       break;
                    case 3: IntelligenceNum.color = Color.white;  break;
                    case 4: TechNum.color = Color.white;          break;
                    case 5: DiplomacyNum.color = Color.white;     break;
                    case 6: LoyaltyNum.color = Color.white;       break;
                }
            }
        }
    }

    public void ToggleInventory()
    {
        if (!isUIActive)
        {
            AttributePanel.SetActive(true);
            isUIActive = true;
        }
        else if (isUIActive)
        {
            AttributePanel.SetActive(false);
            isUIActive = false;
        }

        if (!isInventoryActive)
        {
            InventoryPanel.SetActive(true);
            isInventoryActive = true;
            ItemsPanel.SetActive(true);
        }
        else if (isInventoryActive)
        {
            InventoryPanel.SetActive(false);
            isInventoryActive = false;
            ItemsPanel.SetActive(false);
            Tooltip.CloseToolTip();
        }
    }
    public bool IsInventoryActice()
    {
        if (!isInventoryActive)
        {
            return false;
        }
        return true;
    }
    public void ShowLoot(bool active)
    {
        if (active == false)
        {
            LootPanel.SetActive(false);
        }
        else if (active == true)
        {
            LootPanel.SetActive(true);
        }
    }

    public TextMeshProUGUI AssignLootText(int index)
    {
        TextMeshProUGUI itemSlot = Item_1.GetComponent< TextMeshProUGUI>();

        if (index == 1)
            itemSlot = Item_1.GetComponentInChildren<TextMeshProUGUI>();
        if (index == 2)
            itemSlot = Item_2.GetComponentInChildren<TextMeshProUGUI>();
        if (index == 3)
            itemSlot = Item_3.GetComponentInChildren<TextMeshProUGUI>();

        return itemSlot;
    }

    public void OpenVendorPanel()
    {
        VendorPanel.SetActive(true);
        ItemsPanel.SetActive(true);
        AttributePanel.SetActive(true);
        InventoryPanel.SetActive(true);
        isInventoryActive = true;
        isUIActive = true;
    }


    public void CloseVendorPanel()
    {
        VendorPanel.SetActive(false);
        ItemsPanel.SetActive(false);
        AttributePanel.SetActive(false);
        InventoryPanel.SetActive(false);
        isInventoryActive = false;
        isUIActive = false;
        Player.instance.playerState = PlayerStates.MOVING;
    }
}
