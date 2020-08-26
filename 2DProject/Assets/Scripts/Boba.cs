using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boba : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            heal = heal / 10;
            collision.transform.GetComponent<PlayerScript>().Heal(heal);
            Destroy(this.gameObject);
        }
    }
}
