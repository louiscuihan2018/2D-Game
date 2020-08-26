using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region movement_variables
    public float movespeed;
    #endregion

    #region physcis_components
    Rigidbody2D enemyRB;
    #endregion

    #region targeting_variables
    public Transform player;
    #endregion

    #region health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion
    [SerializeField]
    [Tooltip("Boba")]
    private GameObject Boba;
    [SerializeField]
    [Tooltip("Coffee")]
    private GameObject Coffee;
    #region Unity_functions
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }
    private void Update()
    {
        if (player == null)
        {
            return;
        }

        Move();
    }
    #endregion

    #region movement_functions
    private void Move()
    {
        Vector2 direction = player.position - transform.position;
        enemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region attack_functions
    private void Explode()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(explosionDamage);
                Debug.Log("Hit player with explosion");
                Instantiate(explosionObj, transform.position, transform.rotation);

            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
    #endregion

    #region health_functions
    public void TakeDamage(float value)
    {
        currHealth -= value;
        Debug.Log("health is now" + " " + currHealth.ToString());

        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
        System.Random rnd = new System.Random();
        int test = rnd.Next(1, 3);
        if (test == 1)
        {
            Instantiate(Boba, transform.position, transform.rotation);
        }
        else if (test == 2)
        {
            Instantiate(Coffee, transform.position, transform.rotation);
        }
    }
    #endregion

}
