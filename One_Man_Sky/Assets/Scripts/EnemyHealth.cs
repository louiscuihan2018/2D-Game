using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    #region health_variables
    public Slider EnemyHP;
    public Vector3 offset;
    public float startingHealth = 10.0f;
    public float currentHealth;
    #endregion

    #region damage_reduction_variables
    [Tooltip("This value reduces incoming laser damage. (Default: 0)")]
    public float laserDamageReduction = 0;

    [Tooltip("This value reduces incoming explosive damage. (Default: 0)")]
    public float ordinanceDamageReduction = 0;

    [Tooltip("This value reduces incoming collision damage. (Default: 0)")]
    public float collisionDamageReduction = 0;
    #endregion

    #region explosion_effects
    public GameObject explosion;
    public GameObject debris;
    #endregion

    #region health_functions
    void Awake()
    {
        currentHealth = startingHealth;
        EnemyHP.value = currentHealth / startingHealth;
        offset = EnemyHP.transform.position - transform.position;
    }

    void Update()
    {
        Quaternion a = EnemyHP.transform.rotation;
        EnemyHP.transform.rotation *= Quaternion.Inverse(a);
        EnemyHP.transform.position = transform.position + offset;
    }

    public void TakeDamage(float laserDamage, float ordinanceDamage, float collisionDamage)
    {
        // Reduce the current health by the damage amount.
        float amount = laserDamage - laserDamageReduction + ordinanceDamage - ordinanceDamageReduction + collisionDamage - collisionDamageReduction;

        currentHealth -= amount;
        StartCoroutine(damageAnimation());

        //Debug
        //Debug.Log("Enemy Hp is now" + currentHealth.ToString());

        // If the player has lost all it's health and the death flag hasn't been set yet...

        EnemyHP.value = currentHealth / startingHealth;

        if (currentHealth <= 0)
        {
            // ... it should die.
            Death();
        }
    }

    public void TakeDamageOverTime(float damage, float totalDamage)
    {
        StartCoroutine(damageOverTime(damage, totalDamage));
    }

    public void Repair(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, startingHealth);

        EnemyHP.value = currentHealth / startingHealth;
        //Debug
        //Debug.Log("Enemy Hp is now" + currentHealth.ToString());
    }

    void Death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(debris, transform.position, transform.rotation);
        GameObject loot = LootTable.instance.getLoot();
        if (loot != null)
        {
            Instantiate(loot, transform.position, transform.rotation);
        }
        ScoreScript.Singleton.AddScore(1);
        Destroy(this.gameObject);
    }
    #endregion
  
    public void Position()
    {
        Vector2 currentPos = transform.position;
    }
    #region health_coroutines
    public IEnumerator damageOverTime(float damage, float overallDamage)
    {
        float damageTaken = 0;
        while (damageTaken < overallDamage)
        {
            this.TakeDamage(damage, 0, 0);
            damageTaken += damage;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator damageAnimation()
    {
        float flashTimer = 0;
        Color flashColor = new Color(1, 0.3f, 0.3f);
        while (flashTimer < 0.5f)
        {
            this.GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            flashTimer += 0.2f;
        }
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    #endregion
}
