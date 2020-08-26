using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit_1 : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
        SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
    }
    }
}
