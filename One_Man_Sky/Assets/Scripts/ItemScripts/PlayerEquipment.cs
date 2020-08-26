using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    #region equipment_variables
    EquipmentManager manager;
    public Weapon defaultWeapon;
    public Ordinance defaultOrdinance;
    public Special defaultSpecial;
    public Hull defaultHull;
    public Shield defaultShield;
    public Engine defaultEngine;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        manager = EquipmentManager.instance;
        manager.autoEquip(defaultWeapon);
        manager.autoEquip(defaultOrdinance);
        manager.autoEquip(defaultSpecial);
        manager.autoEquip(defaultHull);
        manager.autoEquip(defaultShield);
        manager.autoEquip(defaultEngine);
        synchronizeAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.hasUpdated)
        {
            synchronizeAll();
            manager.hasUpdated = false;
        }
    }
    #endregion

    #region synchronization
    public void synchronizeAll()
    {
        applyWeapon();
        applyOrdinance();
        applySpecial();
        applyHull();
        applyShield();
        applyEngine();
    }

    void applyWeapon()
    {
        Weapon weapon = (Weapon) manager.currentEquipment[0];
        if (weapon == null)
        {
            weapon = defaultWeapon;
        }
        GameObject weaponGO = weapon.itemObject;
        this.GetComponent<PlayerAttack>().fireDelay = weapon.fireDelay;
        this.GetComponent<PlayerAttack>().projectilePrefab = weaponGO;
    }

    void applyOrdinance()
    {
        Ordinance ordinance = (Ordinance) manager.currentEquipment[1];
        if (ordinance == null)
        {
            ordinance = defaultOrdinance;
        }
        GameObject ordinanceGO = ordinance.itemObject;
        this.GetComponent<PlayerAttack>().ordinancePrefab = ordinanceGO;
        this.GetComponent<PlayerAttack>().launchDelay = ordinance.launchDelay;
    }

    void applySpecial()
    {
        Special special = (Special)manager.currentEquipment[2];
        if (special == null)
        {
            special = defaultSpecial;
        }
        this.GetComponent<PlayerHealth>().canRegenerate = special.canRegenerate;
        this.GetComponent<PlayerHealth>().regenerationTotal = special.regenerationTotal;
        this.GetComponent<PlayerHealth>().regenerationCooldown = special.regenerationCooldown;
        this.GetComponent<PlayerAttack>().canOvercharge = special.canOvercharge;
        this.GetComponent<PlayerAttack>().overchargeRate = special.overchargeRate;
        this.GetComponent<PlayerAttack>().overchargeDuration = special.overchargeDuration;
        this.GetComponent<PlayerAttack>().overchargeCooldown = special.overchargeCooldown;
    }

    void applyHull()
    {
        Hull hull = (Hull)manager.currentEquipment[3];
        if (hull == null)
        {
            hull = defaultHull;
        }
        this.GetComponent<PlayerHealth>().startingHealth = hull.health;
        if (this.GetComponent<PlayerHealth>().currentHealth > hull.health)
        {
            this.GetComponent<PlayerHealth>().currentHealth = hull.health;
        }
        this.GetComponent<PlayerHealth>().laserDamageReduction = hull.laserDamageReduction;
        this.GetComponent<PlayerHealth>().ordinanceDamageReduction = hull.ordinanceDamageReduction;
        this.GetComponent<PlayerHealth>().collisionDamageReduction = hull.collisionDamageReduction;
    }

    void applyShield()
    {
        Shield shield = (Shield)manager.currentEquipment[4];
        if (shield == null)
        {
            shield = defaultShield;
        }

    }

    void applyEngine()
    {
        Engine engine = (Engine)manager.currentEquipment[5];
        if (engine == null)
        {
            engine = defaultEngine;
        }
        this.GetComponent<PlayerMovement>().topSpeed = engine.topSpeed;
        this.GetComponent<PlayerMovement>().shipAccel = engine.shipAccel;
        this.GetComponent<PlayerMovement>().boostCooldownTime = engine.boostCooldownTime;
        this.GetComponent<PlayerMovement>().boostAccelModifier = engine.boostAccelModifier;
        this.GetComponent<PlayerMovement>().boostDuration = engine.boostDuration;
        this.GetComponent<PlayerMovement>().boostStartup = engine.boostStartup;
    }
    #endregion
}
