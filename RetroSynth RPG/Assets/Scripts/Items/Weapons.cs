using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]

public class Weapons : Item
{
    [Header("Item Specific")]

    public WeaponType weaponType = new WeaponType();
    public AbilityBase weaponAbility;
    public GameObject projectile;
    public GameObject changedProjectile;
    public float deleteTime, baseDamage, weaponSpeed, projectileSpeed;

    [Header("Stats")]
    public uint strength;
    public uint dexterity, stamina, intelligence, tech, diplomacy;
    private bool weaponUsed;
    public void UseWeapon()
    {

        weaponUsed = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject GunPoint = GameObject.Find("GunPointPlayer");
        Vector3 newPos = new Vector3(GunPoint.transform.position.x, GunPoint.transform.position.y, GunPoint.transform.position.z);
        GameObject pClone = Instantiate(projectile, newPos, player.transform.rotation);
        pClone.transform.rotation = player.transform.rotation;

        pClone.GetComponent<Projectile>().speed = projectileSpeed;
        pClone.GetComponent<Projectile>().time = deleteTime;

        switch (weaponType)
        {
            case WeaponType.Melee:
                player.GetComponent<PlayerMovement>().PlayAttackAnim(1);
                pClone.GetComponent<Projectile>().damage = baseDamage + (CharacterAttributes.GetModifier(0) / 2);
                break;
            case WeaponType.Ranged:
                player.GetComponent<PlayerMovement>().PlayAttackAnim(0);
                pClone.GetComponent<Projectile>().damage = baseDamage + (CharacterAttributes.GetModifier(1) / 2);
                break;
        }
    }

    public void UseWeapon(GameObject m_projectile)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject GunPoint = GameObject.Find("GunPointPlayer");
        Vector3 newPos = new Vector3(GunPoint.transform.position.x, GunPoint.transform.position.y, GunPoint.transform.position.z);
        GameObject pClone = Instantiate(m_projectile, newPos, player.transform.rotation);
        pClone.transform.rotation = player.transform.rotation;
        
        pClone.GetComponent<Projectile>().speed = projectileSpeed;
        pClone.GetComponent<Projectile>().time = deleteTime;

        switch (weaponType)
        {
            case WeaponType.Melee:
                player.GetComponent<PlayerMovement>().PlayAttackAnim(1);                  
                break;
            case WeaponType.Ranged:
                player.GetComponent<PlayerMovement>().PlayAttackAnim(0);
                break;
        }
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public override void Use()
    {
        if (Player.instance.playerState == PlayerStates.SELLING)
            Sell();
        else
        {
            Inventory.instance.EquipItem(this);
            AbilityManager.instance.LearnAbility(weaponAbility);
        }
    }

    public override void Remove()
    {
        Inventory.instance.MoveToInventory(this);
        AbilityManager.instance.UnlearnAbility(weaponAbility);
    }

    public override void AddStatistics()
    {
        CharacterAttributes.AddModifier(0, strength);
        CharacterAttributes.AddModifier(1, dexterity);
        CharacterAttributes.AddModifier(2, stamina);
        CharacterAttributes.AddModifier(3, intelligence);
        CharacterAttributes.AddModifier(4, tech);
        CharacterAttributes.AddModifier(5, diplomacy);
        UI.instance.UpdateAttributes();
    }
    public override void RemoveStatistics()
    {
        CharacterAttributes.RemoveModifier(0, strength);
        CharacterAttributes.RemoveModifier(1, dexterity);
        CharacterAttributes.RemoveModifier(2, stamina);
        CharacterAttributes.RemoveModifier(3, intelligence);
        CharacterAttributes.RemoveModifier(4, tech);
        CharacterAttributes.RemoveModifier(5, diplomacy);
        UI.instance.UpdateAttributes();
    }

    public override int GetItemType()
    {
        return 6;
    }


}
public enum WeaponType
{
    Melee, Ranged
};

