using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotcollision : MonoBehaviour
{
    public List<GameObject> dotlist;
    void OnTriggerEnter2D(Collider2D coll) 
    {
        dotlist.Add(coll.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dotlist.Remove(collision.gameObject);
    }
    private void Update()
    {
        List<GameObject> dotlistclone = new List<GameObject>(dotlist);
        foreach (GameObject dotted in dotlistclone)
        {
            if (dotted == null)
            {
                dotlist.Remove(dotted);
                continue;
            }
            if (dotted.CompareTag("Enemy"))
            {
                dotted.GetComponent<EnemyHealth>().TakeDamage(2f, 2f, 2f);
                
            }
            if (dotted.CompareTag("Ally"))
            {
                dotted.GetComponent<EnemyHealth>().TakeDamage(2f, 2f, 2f);

            }
            if (dotted.CompareTag("Player"))
            {
                dotted.GetComponent<PlayerHealth>().TakeDamage(.3f, .3f, .3f);
            }
            if (dotted == null)
            {
                dotlist.Remove(dotted);
            }
        }
    }
}
