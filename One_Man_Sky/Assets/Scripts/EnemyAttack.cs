using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region weapon_variables
    public Transform leftWeapon;
    public Transform rightWeapon;

    public Transform leftSecondaryWeapon;
    public Transform rightSecondaryWeapon;
    #endregion

    #region shooting_variables
    float timeSinceFire = 0f;
    float timeSinceLaunch = 0f;

    [Tooltip("This is how fast the enemy can shoot")]
    public float fireDelay = 1f;

    [Tooltip("This is how fast the enemy can launch ordinance")]
    public float launchDelay = 1f;

    [Tooltip("This is the projectile the enemy is shooting")]
    public GameObject projectilePrefab;

    [Tooltip("This is the ordinance this enemy is launching")]
    public GameObject ordinancePrefab;

    public float firingType = 0;
    public float fireAngle = 30f;
    public float maxRange = 60f;

    public Vector3 target;
    #endregion


    #region unity_functions
    private void Start()
    {
        firingType = 0;
    }

    private void Update()
    {
        //Check if target is in firing angle and in firing range
        if (this.GetComponent<EnemyMovement>().target == null)
        {

        } else
        {
            target = this.GetComponent<EnemyMovement>().targetPosition;
            Vector3 direction = target - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.up);
            if (Vector3.Distance(target, this.transform.position) < maxRange && Mathf.Abs(angle) < fireAngle && timeSinceFire > fireDelay)
            {
                StartCoroutine(fire());
                timeSinceFire = 0;
            }
            if (Vector3.Distance(target, this.transform.position) < maxRange && Mathf.Abs(angle) < 2*fireAngle && timeSinceLaunch > launchDelay)
            {
                if (ordinancePrefab != null)
                {
                    Instantiate(ordinancePrefab, transform.position, transform.rotation);
                }
                timeSinceLaunch = 0;
            }
        }
        timeSinceFire += Time.deltaTime;
        timeSinceLaunch += Time.deltaTime;
    }
    #endregion

    #region shootingFunctions
    public void DebuffAttack(float amount)
    {
        if (timeSinceFire > 0)
        {
            timeSinceFire = 0;
        }
        timeSinceFire -= amount;
    }
    #endregion

    #region shootingCoroutines
    IEnumerator fire()
    {
        if (leftWeapon != null)
        {
            Instantiate(projectilePrefab, leftWeapon.position, leftWeapon.rotation);
        }
        if (rightWeapon != null)
        {
            Instantiate(projectilePrefab, rightWeapon.position, rightWeapon.rotation);
        }
        yield return new WaitForSeconds(0.1f);
        if (leftSecondaryWeapon != null)
        {
            Instantiate(projectilePrefab, leftSecondaryWeapon.position, leftSecondaryWeapon.rotation);
        }
        if (rightSecondaryWeapon != null)
        {
            Instantiate(projectilePrefab, rightSecondaryWeapon.position, rightSecondaryWeapon.rotation);
        }
    }
    #endregion
}
