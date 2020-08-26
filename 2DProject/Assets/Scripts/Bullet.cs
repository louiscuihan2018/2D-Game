using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 10f;
    Rigidbody2D rb;
    public float damage;
    public float lifetime;

    GameObject target;
    Vector2 moveDirection;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!isPlayer)
        {
            target = GameObject.Find("Player");
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.CompareTag("Player") && isPlayer == false)
        {
            collision.transform.GetComponent<PlayerScript>().TakeDamage(damage);
            Debug.Log("Hit player with bullet");
        }
        else if (collision.CompareTag("Boss") && isPlayer)
        {
            collision.transform.GetComponent<Denero>().TakeDamage(damage);
        }
        else if (collision.CompareTag("Enemy") && isPlayer){
            collision.transform.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifetime);
        //Destroy(gameObject);
    }
}
