using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int money;
    [SerializeField] private int MaxPsychosis;
    [SerializeField] private int psychosis;

    public PlayerStates playerState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
     

        playerState = new PlayerStates();
        UI.instance.UpdateStatPanel();
        UI.instance.UpdatePlayerStatusBars();
    }

    //---------------- Money -----------------------
    public void AddMoney(int x)
    {
        money += x;
    }

    public void SetMoney(int x)
    {
        money = x;
    }

    public void RemoveMoney(int x)
    {
        money -= x;
    }

    public int GetMoney()
    {
        return money;
    }

    //------------------------- Health ----------------------------
    public void AddHealth(int x)
    {
        currentHealth += x;
    }

    public void SetHealth(int x)
    {
        currentHealth = x;
    }

    public void RemoveHealth(int x)
    {
        currentHealth -= x;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    //------------------------- Max Health ----------------------------
    public void AddMaxHealth(int x)
    {
        maxHealth += x;
    }

    public void SetMaxHealth(int x)
    {
        maxHealth = x;
    }

    public void RemoveMaxHealth(int x)
    {
        maxHealth -= x;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    //------------------------- Psychosis ----------------------------
    public void AddPsychosis(int x)
    {
        psychosis += x;
    }

    public void SetPsychosis(int x)
    {
        psychosis = x;
    }

    public void RemovePsychosis(int x)
    {
        psychosis -= x;
    }

    public int GetPsychosis()
    {
        return psychosis;
    }

    //------------------------- Max Psychosis ----------------------------
    public void AddMaxPsychosis(int x)
    {
        MaxPsychosis += x;
    }

    public void SetMaxPsychosis(int x)
    {
        MaxPsychosis = x;
    }

    public void RemoveMaxPsychosis(int x)
    {
        MaxPsychosis -= x;
    }

    public int GetMaxPsychosis()
    {
        return MaxPsychosis;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            playerState = PlayerStates.COMBAT;
            float p_damage = other.gameObject.GetComponent<Projectile>().damage;
            Debug.Log(p_damage);
            RemoveHealth((int)p_damage);
            UI.instance.UpdateStatPanel();
            UI.instance.UpdatePlayerStatusBars();
        }
    }

}
public enum PlayerStates
{
    LOOTING, COMBAT, EXPLORING, TALKING, MOVING, SELLING
}

