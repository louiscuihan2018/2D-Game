using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region health_variables
    [Tooltip("This value determines the starting health of the player. (Default: 10)")]
    public float startingHealth = 10.0f;
    [Tooltip("This value determines how long the player is invulnerable after taking damage for. (Default: 1)")]
    public float iFrames = 1f;
    public float currentHealth;
    public Slider PlayerHP;

    bool recentlyTookDamage = false;
    public Image SpecialCooldown;
    #endregion

    #region regenerate_variables
    public bool canRegenerate = false;
    public float regenerationTotal;
    public float regenerationCooldown;
    float regenerationTimer;
    bool usedRegenerate;
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

    #region unity_functions
    private void Start()
    {
        if (SpecialCooldown != null)
        {
            SpecialCooldown.fillAmount = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && regenerationTimer > regenerationCooldown && canRegenerate)
        {
            StartCoroutine(HealOverTime(regenerationTotal));
            usedRegenerate = true;
            regenerationTimer = 0;
        }
        regenerationTimer += Time.deltaTime;
        if (SpecialCooldown != null && canRegenerate)
        {
            SpecialCooldown.fillAmount += (1 / regenerationCooldown * Time.deltaTime) ;

            if (SpecialCooldown.fillAmount >= 1 && usedRegenerate == true)
            {
                SpecialCooldown.fillAmount = 0;
            }
            usedRegenerate = false;
        }
    }
    #endregion

    #region health_functions
    void Awake()
    {
        currentHealth = startingHealth;
        // set the slider
        PlayerHP.value = currentHealth / startingHealth;
    }

    public void TakeDamage(float laserDamage, float ordinanceDamage, float collisionDamage)
    {
        // Reduce the current health by the damage amount.
        float amount = laserDamage*(1 - laserDamageReduction) + ordinanceDamage*(1 - ordinanceDamageReduction) + collisionDamage*(1 - collisionDamageReduction);

        if (!recentlyTookDamage)
        {
            currentHealth -= amount;
            recentlyTookDamage = true;
            StartCoroutine(damageAnimation());
        }

        //Debug
        //Debug.Log("Player Hp is now" + currentHealth.ToString());

        // set the slider
        PlayerHP.value = currentHealth / startingHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0)
        {
            // ... it should die.
            StartCoroutine(deathAnimation());
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


        //Debug
        //Debug.Log("Player Hp is now" + currentHealth.ToString());

        // set the slider
        PlayerHP.value = currentHealth / startingHealth;
    }

    void Death()
    {
        SceneManager.LoadScene("RestartScene");
    }
    #endregion

    #region health_coroutines
    IEnumerator damageAnimation()
    {
        float flashTimer = 0;
        Color flashColor = new Color(1, 0.3f, 0.3f);
        while (flashTimer < 1f)
        {
            this.GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.2f);
            flashTimer += 0.4f;
        }
        this.GetComponent<SpriteRenderer>().color = Color.white;
        recentlyTookDamage = false;
            
    }

    public IEnumerator damageOverTime(float damage, float overallDamage)
    {
        float damageTaken = 0;
        while (damageTaken < overallDamage)
        {
            if (!recentlyTookDamage)
            {
                this.TakeDamage(damage, 0, 0);
                damageTaken += damage;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator HealOverTime(float regenerateTotal)
    {
        float healthRegenerated = 0;
        while (healthRegenerated < regenerateTotal)
        {
            Repair(Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            healthRegenerated += Time.deltaTime;
        }
    }

    IEnumerator deathAnimation()
    {
        this.GetComponent<PlayerMovement>().enabled = false;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(0.5f);
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(debris, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);

        this.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1f;
        Death();
    }
    #endregion
}