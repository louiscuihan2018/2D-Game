using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    #region shooting_variables
    private bool canShoot;
    private float lastFire;

    float fireDelay;

    GameObject projectilePrefab;
    #endregion

    #region unity_functions

    private void Start()
    {
        canShoot = true;
        lastFire = 0.0f;
        fireDelay = this.GetComponentInParent<EnemyFire>().fireDelay;
        projectilePrefab = this.GetComponentInParent<EnemyFire>().projectilePrefab;

    }
    void Update()
    {
        bool openFire = this.GetComponentInParent<EnemyFire>().openFire;
        if (Time.time > lastFire + fireDelay && openFire)
        {
            Shoot();
            lastFire = Time.time;
        }
    }

    #endregion

    #region shooting_functions

    public void Shoot()
    {
        if (!canShoot)
        {
            return;
        }
        GameObject bullet = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
        float heading = Mathf.Atan2(transform.right.z, transform.right.x) * Mathf.Rad2Deg;
        //bullet.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
    }
    #endregion
}
