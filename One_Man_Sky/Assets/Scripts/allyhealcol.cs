using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allyhealcol : MonoBehaviour
{
    public List<GameObject> playerinrange;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in playerinrange) {
            player.GetComponent<PlayerHealth>().Repair(.02f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerinrange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerinrange.Remove(collision.gameObject);
        }
    }
}
