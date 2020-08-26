using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    public GameObject projectile1;
    public float projspeed;
    public float firerate;
    private float firetimer;
    List<GameObject> targetlist;
    private void Awake()
    {
        targetlist = new List<GameObject>();
        firetimer = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        firetimer += Time.deltaTime;
        if (firetimer > firerate)
        {
            if (targetlist.Count > 0)
            {
                int target = Random.Range(0, targetlist.Count);
                Vector2 FireDirection = transform.position - targetlist[target].transform.position;
                FireDirection = FireDirection.normalized * projspeed;
                GameObject newProjectile = (GameObject)Instantiate(projectile1, transform.position, transform.rotation);
                newProjectile.GetComponent<Rigidbody2D>().velocity = FireDirection;
                firetimer = 0;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.gameObject.CompareTag("Player"))
        {
            targetlist.Add(collided.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            targetlist.Remove(collision.gameObject);
        }
    }

}
