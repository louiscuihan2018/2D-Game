using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment/Special")]
public class Special : Equipment
{
    #region regenerate_special
    public bool canRegenerate;
    public float regenerationTotal;
    public float regenerationCooldown;
    #endregion

    #region invulnerability_special
    public bool canInvulnerable;
    public float invulnerabilityDuration;
    #endregion

    #region overcharge_special
    public bool canOvercharge;
    public float overchargeRate;
    public float overchargeDuration;
    public float overchargeCooldown;
    #endregion
}
