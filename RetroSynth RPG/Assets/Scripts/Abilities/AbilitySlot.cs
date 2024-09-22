using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySlot : MonoBehaviour
{
    public AbilityBase ability;
    private Image displayImage;
    private TextMeshProUGUI cooldownText;

    // Start is called before the first frame update
    void Start()
    {
        updateInfo();
       
    }

    public void updateInfo()
    {
        displayImage = transform.Find("Image").GetComponent<Image>();
        cooldownText = transform.Find("cooldownText").GetComponent<TextMeshProUGUI>();
        if (ability)
        {
            displayImage.sprite = ability.icon;
            displayImage.color = Color.white;
        }
        else
        {
            displayImage.sprite = null;
            displayImage.color = Color.clear;

        }
    }

    private void Update()
    {
        if (ability)
        {
            if (ability.IsOnCooldown())
            {
                displayImage.sprite = null;
                displayImage.color = Color.clear;
                cooldownText.text = ability.UpdateCooldown().ToString();
            }
            else
            {
                displayImage.sprite = ability.icon;
                displayImage.color = Color.white;
                cooldownText.text = "";
            }
        }
    }

    public void UseAbility() // use from UI
    {
        if (ability)
        {
            if (ability.IsOnCooldown())
            {
                FloatingText.DisplayFloatingText("Ability on Cooldown");
            }
            else
            {
                ability.Use();
            }
        }
    }
}
