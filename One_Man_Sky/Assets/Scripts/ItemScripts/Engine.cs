using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Engine")]
public class Engine : Equipment
{
    public float topSpeed;
    public float shipAccel;
    public float boostCooldownTime;
    public float boostAccelModifier;
    public float boostStartup;
    public float boostDuration;
}
