using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region player_variables
    GameObject player;
    #endregion

    #region fixed_movement_variables
    float enemyDecel = 0.5f;
    float enemyAccel = 1;
    #endregion

    #region movement_variables
    [Tooltip("The current speed of the enemy. (Default = 0)")]
    public float enemySpeed = 0f;
    [Tooltip("The top speed of the enemy. (Default = ?)")]
    public float topSpeed = 45f;
    [Tooltip("The distance at which the enemy swerves away. (Default = 20)")]
    public float swerveDistance = 20f;
    [Tooltip("The distance at which the enemy begins accelerating. (Default = 80)")]
    public float accelerateDistance = 80f;
    [Tooltip("The distance at which the enemy begins decelerating. (Default = 40)")]
    public float decelerateDistance = 40f;

    bool swerving = false;
    public GameObject target;
    public Vector3 targetPosition;
    #endregion

    #region turning_variables
    public int targetingType = 2;
    public float rotateSpeed = 3;
    Vector3 currentAngle;
    float swerveTimer;
    public float swerveDuration = 2f;
    #endregion

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ally");
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

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swerveTimer = swerveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy == null)
            {
                target = player;
            } else
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < Vector3.Distance(closestEnemy.transform.position, this.transform.position))
                {
                    target = player;
                }
                else
                {
                    target = closestEnemy;
                }
            }
            targetPosition = target.transform.position;

        }
        else
        {
            setSpeed();
            moveForward();
            turn();
        }
    }
    #endregion

    #region movement_functions
    void setSpeed()
    {
        //If reversing, stop.
        if (enemySpeed < 0)
        {
            enemySpeed = 0;
        }

        //If too fast, slow down.
        if (enemySpeed > topSpeed)
        {
            enemySpeed -= enemyDecel;
        }

        // If swerving, speed up and run away.
        if ((target.transform.position - transform.position).magnitude < swerveDistance && enemySpeed < topSpeed)
        {
            enemySpeed += enemyAccel;
            swerving = true;
        }

        // If in pursuit but far away, speed up.
        if (!swerving && (target.transform.position - transform.position).magnitude > accelerateDistance && enemySpeed < topSpeed)
        {
            enemySpeed += enemyAccel;
        }

        // If close enough and in pursuit, decelerate.
        if (!swerving && (target.transform.position - transform.position).magnitude < decelerateDistance)
        {
            enemySpeed -= enemyDecel;
        }
        
    }

    void moveForward()
    {
        transform.Translate(Vector3.up * Time.deltaTime * enemySpeed);
    }


    public void DebuffSpeed(float amount)
    {
        enemySpeed -= amount;
        if (enemySpeed < 0)
        {
            enemySpeed = 0;
        }
    }
    #endregion

    #region turning_functions
    //Note: 
    // When deciding look rotation, there are three main modes.
    // transform.position - player.transform.position: Causes avoidance, seemingly perpindicular. Causes a circling behavior.
    // player.transform.position - transform.position: Causes tracking.
    // player.transform.position + transform.position: Causes flyby/collision. They will typically overshoot and loop back around.

    //-player.transform.position: Causes weird figure eight behavior, similar to above.

    void turn()
    {
        if (swerving)
        {
            swerve();
            return;
        }
        if (targetingType == 0)
        {
            targetPosition = target.transform.position;
            trackTarget();
        }
        else if (targetingType == 1)
        {
            if (target == player)
            {
                targetPosition = (target.transform.position + target.transform.up * player.GetComponent<PlayerMovement>().currentSpeed);
            } else
            {
                targetPosition = (target.transform.position + target.transform.up * target.GetComponent<AlliedMovement>().allySpeed);
            }
            
            trackTarget();
        }
    }

    void swerve()
    {
        if (swerveTimer < 0)
        {
            swerving = false;
            swerveTimer = swerveDuration;
        }
        var newRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
        swerveTimer -= Time.deltaTime;
    }

    void trackTarget()
    {
        var newRotation = Quaternion.LookRotation(transform.position - targetPosition, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * rotateSpeed*100);
    }
    #endregion
}
