using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment/Shield")]
public class Shield : Equipment
{
    public float shieldHealth;
    public float rechargeRate;
    public float rechargeDelay;
}
