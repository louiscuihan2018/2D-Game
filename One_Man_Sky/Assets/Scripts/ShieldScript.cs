using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    #region shield_variables
    public float health = 10f;
    public float lifeTimer = 2f;
    #endregion

    #region unity_function
    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position = GameObject.Find("Player").transform.position;
    }
    #endregion

    #region shield_functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack") {
            if (collision.gameObject.GetComponent<MissileScript>())
            {
                TakeDamage(collision.gameObject.GetComponent<MissileScript>().damage);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.GetComponent<LaserScript>())
            {
                TakeDamage(collision.gameObject.GetComponent<LaserScript>().damage);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "Enemy") {
                TakeDamage(10);
            }
        }
    }

    private void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0)
        {
            Destroy(this);
        }
    }
    #endregion
}
