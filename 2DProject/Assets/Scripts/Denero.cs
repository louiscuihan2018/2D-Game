using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denero : MonoBehaviour
{
    [SerializeField]
    
    float fireRate;
    float nextFire;
    #region movement_variables
    public float movespeed;
    #endregion

    public GameObject bullet;

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

    Vector2 currDirection;
    Animator anim;

    #region Unity_functions
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        fireRate = 0.5f;
        nextFire = Time.time;
    }
    private void Update()
    {

        Move();
        CheckIfTimeToFire();
    }
    #endregion

    #region movement_functions
    private void Move()
    {
        System.Random rnd = new System.Random();
        int test = rnd.Next(1, 100);
        //anim.SetBool("Moving", true);
        if (test == 96)
        {
            enemyRB.velocity = Vector2.right * 4 * movespeed;
            currDirection = Vector2.right;
        }
        else if (test == 67)
        {
            enemyRB.velocity = Vector2.left * 4 * movespeed;
            currDirection = Vector2.left;
        }
        else if (test == 44)
        {
            enemyRB.velocity = Vector2.up * 4 * movespeed;
            currDirection = Vector2.up;
        }
        else if (test == 9)
        {
            enemyRB.velocity = Vector2.down * 4 * movespeed;
            currDirection = Vector2.down;
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
            //anim.SetBool("Moving", false);
        }

        //anim.SetFloat("DirX", currDirection.x);
        //anim.SetFloat("DirY", currDirection.y);
        //Vector2 direction = player.position - transform.position;
        //enemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region health_functions
    public void TakeDamage(float value)
    {
        currHealth -= value;
        Debug.Log("Boss Health is now" + " " + currHealth.ToString());

        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            GameObject bull = Instantiate(bullet, transform.position, transform.rotation) as GameObject;

            bull.GetComponent<Bullet>().isPlayer = false;

            nextFire = Time.time + fireRate;
        }
    }
}
