using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour { 

    public float lifetime;
    Rigidbody2D rb;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DeathDelay());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit LMAO");

            collision.transform.GetComponent<Enemy>().TakeDamage(damage);
            //Destroy(gameObject);

        }
        if (collision.CompareTag("Boss"))
        {
            Debug.Log("Hit BOSS");

            collision.transform.GetComponent<Denero>().TakeDamage(damage);
            //Destroy(gameObject);

        }

    }

    IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
