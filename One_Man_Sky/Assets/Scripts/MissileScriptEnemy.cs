using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class MissileScriptEnemy : MonoBehaviour
{
    #region missile_variables
    [SerializeField] Transform Target;
    [SerializeField] float MoveSpeed = 350f;
    [SerializeField] float RotateSpeed = 2000f;
    [SerializeField] float DebuffSpeed = 0;
    [SerializeField] float DebuffAttack = 0;
    [SerializeField] float lifeTimer = 5;
    [SerializeField] GameObject debugenemy;
    public GameObject explosion;
    public float damage = 3f;
    Rigidbody2D rb;
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

        GameObject enemy = FindClosestEnemy();
        debugenemy = enemy;
        if (enemy == null) {
            rb.velocity = transform.up * MoveSpeed * Time.deltaTime;
            return;
        }
        Target = FindClosestEnemy().transform;

        rb.velocity = transform.up * MoveSpeed * Time.deltaTime;

        Vector3 targetVector = Target.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        rb.angularVelocity = -1 * rotatingIndex * RotateSpeed * Time.deltaTime;
    }
    #endregion

    #region missile_functions
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        GameObject[] tag1 = GameObject.FindGameObjectsWithTag("Ally");
        GameObject[] tag2 = GameObject.FindGameObjectsWithTag("Player");
        tag1.Concat(tag2).ToArray();
        gos = tag1;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ally") || collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damage, 0, 0);
            {
                collision.transform.GetComponent<EnemyMovement>().DebuffSpeed(DebuffSpeed);
                collision.transform.GetComponent<EnemyAttack>().DebuffAttack(DebuffAttack);
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    #endregion
}
