using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileScript : MonoBehaviour
{
    #region missile_variables
    [SerializeField] Transform Target;
    [SerializeField] float MoveSpeed = 350f;
    [SerializeField] float RotateSpeed = 2000f;
    public float damage = 3f;
    [SerializeField] float DebuffSpeed = 0;
    [SerializeField] float DebuffAttack = 0;
    [SerializeField] float lifeTimer = 5;
    public GameObject explosion;
    Rigidbody2D rb;
    public bool isPlayer;
    #endregion

    #region unity_functions
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
       
    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        GameObject target = null;
        if (isPlayer)
        {
            target = FindClosestEnemy();
        } else
        {
            target = FindClosestAllyOrPlayer();
        }
        if (target == null) {
            rb.velocity = transform.up * MoveSpeed * Time.deltaTime;
            return;
        }

        rb.velocity = transform.up * MoveSpeed * Time.deltaTime;

        Vector3 targetVector = target.transform.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        rb.angularVelocity = -1 * rotatingIndex * RotateSpeed * Time.deltaTime;
    }
    #endregion

    #region missile_functions
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public GameObject FindClosestAllyOrPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ally");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (closest != null)
        {
            if ((closest.transform.position - position).magnitude > (player.transform.position - position).magnitude)
            {
                closest = player;
            }
        } else
        {
            closest = player;
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isPlayer)
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damage, 0, 0);
            {
                collision.transform.GetComponent<EnemyMovement>().DebuffSpeed(DebuffSpeed);
                collision.transform.GetComponent<EnemyAttack>().DebuffAttack(DebuffAttack);
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ally") && !isPlayer)
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damage, 0, 0);
            {
                collision.transform.GetComponent<AlliedMovement>().DebuffSpeed(DebuffSpeed);
                collision.transform.GetComponent<AlliedAttack>().DebuffAttack(DebuffAttack);
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && !isPlayer)
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage, 0, 0);
            {
                collision.transform.GetComponent<PlayerMovement>().DebuffSpeed(DebuffSpeed);
                collision.transform.GetComponent<PlayerAttack>().DebuffAttack(DebuffAttack);
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    #endregion
}
