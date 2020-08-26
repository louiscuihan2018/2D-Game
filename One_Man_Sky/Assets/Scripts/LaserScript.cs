using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    #region laser_variables
    float lifeTime = 3f;
    float lifeTimer;

    public float damage;
    public float speed;
    [Tooltip("Type 0: Damage only. Type 1: Debuff speed and weapons. Type 2: Damage over time. (Default: 0)")]
    public float laserType = 0;
    public float DOTdamage = 0;
    public float DOTtotalDamage = 0;
    public float DebuffSpeed = 0;
    public float DebuffAttack = 0;

    public bool isPlayer;
    #endregion

    #region unity_functions
    private void Start()
    {
        lifeTimer = lifeTime;
        this.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        if (isPlayer)
        {
            float playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentSpeed;
            this.GetComponent<Rigidbody2D>().velocity = transform.up * (speed + playerSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlayer == false)
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage, 0, 0);
            if (laserType == 1)
            {
                collision.transform.GetComponent<PlayerMovement>().DebuffSpeed(DebuffSpeed);
                collision.transform.GetComponent<PlayerAttack>().DebuffAttack(DebuffAttack);
            }
            if (laserType == 2)
            {
                collision.transform.GetComponent<PlayerHealth>().TakeDamageOverTime(DOTdamage, DOTtotalDamage);
            }
        }
        else if (collision.CompareTag("Enemy") && isPlayer)
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damage, 0, 0);
            if (laserType == 1)
            {
                collision.transform.GetComponent<EnemyMovement>().DebuffSpeed(DebuffSpeed);
                collision.transform.GetComponent<EnemyAttack>().DebuffAttack(DebuffAttack);
            }
            if (laserType == 2)
            {
                collision.transform.GetComponent<EnemyHealth>().TakeDamageOverTime(DOTdamage, DOTtotalDamage);
            }
        }
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
