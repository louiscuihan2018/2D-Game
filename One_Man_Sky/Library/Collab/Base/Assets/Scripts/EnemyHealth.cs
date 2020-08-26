using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region health_variables
    public float startingHealth = 5.0f;
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
        if (currentHealth <= 0)
        {
            // ... it should die.
            Death();
        }
    }

    public void Repair(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, startingHealth);


        //Debug
        //Debug.Log("Enemy Hp is now" + currentHealth.ToString());
    }

    void Death()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(debris, transform.position, transform.rotation);
        ScoreScript.Singleton.AddScore(1);
        Destroy(this.gameObject);
    }
    #endregion

    #region health_coroutines
    IEnumerator damageAnimation()
    {
        Color defaultColor = this.GetComponent<SpriteRenderer>().color;
        float flashTimer = 0;
        Color flashColor = new Color(1, 0.3f, 0.3f);
        while (flashTimer < 0.5f)
        {
            this.GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().color = defaultColor;
            yield return new WaitForSeconds(0.1f);
            flashTimer += 0.2f;
        }
    }
    #endregion
}
