﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region movement_variables
	public float movespeed;
	float x_input;
	float y_input;
    #endregion

    #region attack_variables
    public float damage;
    public float attackspeed;
    float attackTimer;
    public float hitboxTiming;
    public float endAnimationTiming;
    bool isAttacking;
    Vector2 currDirection;
    #endregion

    #region health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region physcis_components
    Rigidbody2D playerRB;
    #endregion

    #region animation_components
    Animator anim;
    #endregion

    #region Unity_functions
    // called once on creation
    private void Awake()
	{
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackTimer = 0;
        currHealth = maxHealth;
    }
	// called every frame 
	private void Update()
	{
        if (isAttacking)
        {
            return;
        }
		// access the input
		x_input = Input.GetAxisRaw("Horizontal");
		y_input = Input.GetAxisRaw("Vertical");

		Move();

        if (Input.GetKeyDown(KeyCode.J) && attackTimer <= 0)
        {
            Attack();
        }
        else {
            attackTimer -= Time.deltaTime;
        } 
    }
	#endregion

	#region movement_functions
    private void Move()
	{

        anim.SetBool("Moving", true);
        if (x_input > 0)
		{
			playerRB.velocity = Vector2.right * movespeed;
            currDirection = Vector2.right;
		}
        else if (x_input < 0)
		{
			playerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left;
        }
		else if (y_input > 0)
		{
			playerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
        }
		else if (y_input < 0)
		{
			playerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down;
        }
        else
		{
			playerRB.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
		}
        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);
    }
    #endregion

    #region attack_functions
    private void Attack()
    {
        Debug.Log("Attacking now!");
        Debug.Log(currDirection);
        
        StartCoroutine(AttackRoutine());
        attackTimer = attackspeed;

    }
    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        playerRB.velocity = Vector2.zero;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(hitboxTiming);
        Debug.Log("Cast hitbox now");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(playerRB.position + currDirection, Vector2.one, 0f, Vector2.zero, 0);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Tons of damage");
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (hit.transform.CompareTag("Boss"))
            {
                Debug.Log("Tons of damage");
                hit.transform.GetComponent<Denero>().TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(endAnimationTiming);
        // enable the movement after the attacking
        isAttacking = false;
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

    public void Heal(float value)
    {
        currHealth += value;
        currHealth = Mathf.Min(currHealth, maxHealth);
        Debug.Log("Health is now" + currHealth.ToString());
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
