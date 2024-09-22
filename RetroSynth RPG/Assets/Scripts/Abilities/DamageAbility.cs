using UnityEngine;

[CreateAssetMenu(fileName = "new Ability", menuName = "Ability/Damage")]

public class DamageAbility : AbilityBase
{
    [Header("Item Specific")]
    public AbilityType abilityType = new AbilityType();
    public int baseDamage;


    public GameObject a_projectile;

    public override void Use()
    {
        StartCooldown();

        Weapons wep = (Weapons)Inventory.instance.GetEquippedWeapon();
        a_projectile.GetComponent<Projectile>().damage = baseDamage;
        wep.UseWeapon(a_projectile);

    }
}
public enum AbilityType
{ 
    Melee, Range
}
