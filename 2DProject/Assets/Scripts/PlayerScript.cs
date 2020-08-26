using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    #region player_variables
    public float speed;
    Rigidbody2D rigid;
    public GameObject bulletPrefeb;
    public float bulletSpeed;
    private float lastfire;
    public float fireDelay;
    public Text healthText;
    private float health;
    private float max_health;
    private bool canShoot;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        health = 4.0f;
        max_health = 4.0f;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if ( Input.GetKeyDown(KeyCode.Space) && Time.time > lastfire + fireDelay)
        {
            shoot();
            lastfire = Time.time;
        }

        rigid.velocity = new Vector2(horizontal * speed, vertical * speed);
        healthText.text = "HEALTH: " + health;
    }

    void shoot() {
        if (!canShoot)
        {
            return;
        }
        GameObject bullet = Instantiate(bulletPrefeb, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().isPlayer = true;

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            bulletSpeed * rigid.velocity.x / Mathf.Max(0.0001f, rigid.velocity.magnitude),
            bulletSpeed * rigid.velocity.y / Mathf.Max(0.0001f, rigid.velocity.magnitude));
    }

    public void TakeDamage(float value)
    {
        health -= value/100;
        Debug.Log("health is now" + " " + health.ToString());

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        health += value;
        health = Mathf.Min(health, max_health);
        Debug.Log("health is now" + " " + health.ToString());
    }

    public void Power(float value)
    {
        fireDelay -= value;
        fireDelay = Mathf.Max(fireDelay, 0.09f);
        Debug.Log("attack speed is now" + " " + health.ToString());
    }

    private void Die()
    {
        canShoot = false;
        Destroy(this.gameObject);
    }
}
