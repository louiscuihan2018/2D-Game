using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    #region bomb_variable
    public float lifeTimer = 3f;
    public GameObject explosion;
    public float damage = 3f;
    public float attackRange = 150f;
    //[SerializeField] float DebuffSpeed = 0;
    //[SerializeField] float DebuffAttack = 0;
    private bool HasExploded = false;
    #endregion

    #region unity_function
    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Explode();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    #endregion

    #region explosion_function
    private void Explode() {
        if (!HasExploded)
        {
            HasExploded = true;
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Enemy");
            float distance = attackRange;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    go.transform.GetComponent<EnemyHealth>().TakeDamage(damage, 0, 0);
                }
            }
        }
    }
    #endregion
}
