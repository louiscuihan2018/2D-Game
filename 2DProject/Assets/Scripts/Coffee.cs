using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            speed = speed / 10;
            collision.transform.GetComponent<PlayerScript>().Power(speed);
            Destroy(this.gameObject);
        }
    }
}
