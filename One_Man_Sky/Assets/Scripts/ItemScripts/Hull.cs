using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment/Hull")]
public class Hull : Equipment
{
    public float health;
    public float laserDamageReduction;
    public float ordinanceDamageReduction;
    public float collisionDamageReduction;
}
