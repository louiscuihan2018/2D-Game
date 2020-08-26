using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GetComponentInParent<Denero>().player = coll.transform;
            Debug.Log("Denero SEE PLAYER");
        }
    }
}
