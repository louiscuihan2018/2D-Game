using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float damage;
    public bool isPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && isPlayer == false)
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(0, 0, damage);
            this.transform.GetComponent<EnemyHealth>().TakeDamage(0, 0, damage);
        }
        //else if (collision.CompareTag("Boss") && isPlayer)
        //{
        //    collision.transform.GetComponent<Denero>().TakeDamage(damage);
        //}
        else if (collision.gameObject.name == "Enemy" && isPlayer)
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(0, 0, damage);
            this.transform.GetComponent<PlayerHealth>().TakeDamage(0, 0, damage);
        }
    }
}
