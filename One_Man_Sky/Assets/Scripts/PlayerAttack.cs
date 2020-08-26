using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    #region weapon_variables
    public Transform leftWeapon;
    public Transform rightWeapon;

    public Image WeaponCooldown;
    public Image OrdinanceCooldown;
    public Image SpecialCooldown;
    bool shotWeapon;
    bool shotOrdinance;
    #endregion

    #region shooting_variables
    float timeSinceFire = 0.0f;
    float timeSinceLaunch = 0.0f;

    [Tooltip("This is how fast the player can shoot a weapon")]
    public float fireDelay;

    [Tooltip("This is how fast the player can launch ordinance.")]
    public float launchDelay;

    [Tooltip("This is the projectile the player is shooting")]
    public GameObject projectilePrefab;

    [Tooltip("This is the ordinance this player is launching")]
    public GameObject ordinancePrefab;
    #endregion

    #region overcharge_variables
    bool usedOvercharge;
    public bool canOvercharge;
    public float overchargeRate;
    public float overchargeDuration;
    public float overchargeCooldown = 1;
    public float overchargeTimer = 10f;
    #endregion

    #region unity_functions
    private void Start()
    {
        if (WeaponCooldown != null)
        {
            WeaponCooldown.fillAmount = 0;
        }
        if (OrdinanceCooldown != null)
        {
            OrdinanceCooldown.fillAmount = 0;
        }
        if (SpecialCooldown != null)
        {
            SpecialCooldown.fillAmount = 0;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && timeSinceFire > fireDelay)
        {
            Shoot();
            timeSinceFire = 0;
            shotWeapon = true;
        }
        if (Input.GetKey(KeyCode.O) && timeSinceLaunch > launchDelay)
        {
            LaunchOrdinance();
            timeSinceLaunch = 0;
            shotOrdinance = true;
        }
        if (Input.GetKey(KeyCode.J) && canOvercharge && overchargeTimer > overchargeCooldown)
        {
            usedOvercharge = true;
            StartCoroutine(Overcharge());
        }
        overchargeTimer += Time.deltaTime;
        timeSinceFire += Time.deltaTime;
        timeSinceLaunch += Time.deltaTime;

       
        WeaponCooldown.fillAmount += (1 / fireDelay * Time.deltaTime);

        if (WeaponCooldown.fillAmount >= 1 && shotWeapon == true)
        {
            WeaponCooldown.fillAmount = 0;
        }

        OrdinanceCooldown.fillAmount += (1 / launchDelay * Time.deltaTime) ;

        if (OrdinanceCooldown.fillAmount >= 1 && shotOrdinance == true)
        {
            OrdinanceCooldown.fillAmount = 0;
        }

        if (SpecialCooldown != null && canOvercharge)
        {
            SpecialCooldown.fillAmount += (1 / overchargeCooldown * Time.deltaTime) ;
            if (SpecialCooldown.fillAmount >= 1 && usedOvercharge == true)
            {
                SpecialCooldown.fillAmount = 0;
                overchargeTimer = 0;
            }
            usedOvercharge = false;
        }
        
        shotWeapon = false;
        shotOrdinance = false;
    }

    #endregion

    #region shooting_functions

    void Shoot() {
        if (projectilePrefab == null)
        {
            return;
        }
        Instantiate(projectilePrefab, leftWeapon.position, leftWeapon.rotation);
        Instantiate(projectilePrefab, rightWeapon.position, rightWeapon.rotation);
    }

    void LaunchOrdinance()
    {
        if (ordinancePrefab == null)
        {
            return;
        }
        Instantiate(ordinancePrefab, this.transform.position, this.transform.rotation);
    }

    public void DebuffAttack(float amount)
    {
        if (timeSinceFire > 0)
        {
            timeSinceFire = 0;
        }
        timeSinceFire -= amount;
    }
    #endregion

    #region overcharge_Coroutine
    IEnumerator Overcharge()
    {
        float tempFireDelay = fireDelay;
        this.fireDelay = fireDelay / overchargeRate;
        yield return new WaitForSeconds(overchargeDuration);
        this.fireDelay = tempFireDelay;

    }
    #endregion
}
