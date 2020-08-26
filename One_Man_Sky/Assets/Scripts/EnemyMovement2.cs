using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    #region boid_variables
    public List<GameObject> neighbors;
    float distFromNeighbors = 10f;
    #endregion

    #region state_variables
    bool inPursuit;
    bool inPatrol;
    #endregion

    #region movement_variables
    public float enemySpeed = 30f;
    //float topSpeed = 45f;
    #endregion

    #region turning_variables
    public float rotateSpeed = 3;
    #endregion

    #region player_variables
    public GameObject player;
    float playerDestination;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        getDirection();
        moveForward();
    }
    #endregion

    #region boid_functions
    Vector3 keepAwayFromNeighbors()
    {
        Vector3 returnVector = new Vector3(0, 0, 0);
        int numTooClose = 0;

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (Vector3.Distance(transform.position, neighbors[i].transform.position) < distFromNeighbors)
            {
                returnVector += (transform.position - neighbors[i].transform.position);
                numTooClose++;
            }
        }
        return - returnVector / (numTooClose + 1);
    }

    Vector3 alignToNeighbors()
    {
        Vector3 returnVector = new Vector3(0, 0, 0);

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (Vector3.Distance(transform.position, neighbors[i].transform.position) < distFromNeighbors)
            {
                returnVector += neighbors[i].transform.up;
            }
        }
        return returnVector / (neighbors.Count + 1);
    }

    Vector3 groupWithNeighbors()
    {
        Vector3 returnVector = new Vector3(0, 0, 0);

        for (int i = 0; i < neighbors.Count; i++)
        {
            returnVector += neighbors[i].transform.position;
        }
        return returnVector / (neighbors.Count + 1);
    }
    #endregion

    #region turning_functions
    void getDirection()
    {
        Vector3 directionVector = ((keepAwayFromNeighbors() + alignToNeighbors() + groupWithNeighbors() + getDestination())/4) - transform.position;
        var directionRotation = Quaternion.LookRotation(transform.position - player.transform.position - directionVector, Vector3.forward);
        directionRotation.x = 0;
        directionRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, directionRotation, Time.deltaTime * rotateSpeed);
    }
    #endregion

    #region movement_functions
    Vector3 getDestination()
    {
        Vector3 playerDestination = player.transform.position + player.transform.up * player.GetComponent<PlayerMovement>().currentSpeed;
        return playerDestination;
    }

    void moveForward()
    {
        transform.Translate(Vector3.up * Time.deltaTime * enemySpeed);
    }

    void adjustSpeed()
    {
    }
    #endregion
}
