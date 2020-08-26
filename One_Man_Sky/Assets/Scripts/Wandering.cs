using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{

    GameObject target;

    [Tooltip("The current speed of the enemy. (Default = 0)")]
    public float enemySpeed = 0f;
    [Tooltip("The top speed of the enemy. (Default = ?)")]
    public float topSpeed = 45f;
    [Tooltip("The distance at which the enemy swerves away. (Default = 20)")]
    public float swerveDistance = 20f;


    public GameObject FindClosest()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Start()
    {
        target = FindClosest();
    }

    void Update()
    {
        getDestination();
        moveForward();
        if (Vector3.Distance(target.transform.position, this.transform.position) < swerveDistance)
        {
            target = FindClosest();
        }

    }

    
    Vector3 getDestination()
    {
        Vector3 Destination = target.transform.position + target.transform.up * target.GetComponent<PlayerMovement>().currentSpeed;
        return Destination;
    }
    void moveForward()
    {
        transform.Translate(Vector3.up * Time.deltaTime * enemySpeed);
    }

}
