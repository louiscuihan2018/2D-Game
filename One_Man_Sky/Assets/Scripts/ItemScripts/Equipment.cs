using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public enum EquipmentSlot { Weapon, Ordinance, Special, Hull, Shield, Engine};
    public EquipmentSlot equipSlot;

    public override void Use()
    {
        EquipmentManager.instance.toggleQuip(this);
    }
}
