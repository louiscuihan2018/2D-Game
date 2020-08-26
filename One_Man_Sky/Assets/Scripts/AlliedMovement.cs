using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedMovement : MonoBehaviour
{
    #region target_variables
    public GameObject target;
    public Vector3 targetPosition;
    #endregion

    #region fixed_movement_variables
    float enemyDecel = 0.5f;
    float enemyAccel = 1;
    #endregion

    #region movement_variables
    [Tooltip("The current speed of the enemy. (Default = 0)")]
    public float allySpeed = 0f;
    [Tooltip("The top speed of the enemy. (Default = ?)")]
    public float topSpeed = 45f;
    [Tooltip("The distance at which the enemy swerves away. (Default = 20)")]
    public float swerveDistance = 20f;
    [Tooltip("The distance at which the enemy begins accelerating. (Default = 80)")]
    public float accelerateDistance = 80f;
    [Tooltip("The distance at which the enemy begins decelerating. (Default = 40)")]
    public float decelerateDistance = 40f;

    bool swerving = false;
    
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


    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        target = FindClosestEnemy();
        swerveTimer = swerveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = FindClosestEnemy();
        } else
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
        if (allySpeed < 0)
        {
            allySpeed = 0;
        }

        //If too fast, slow down.
        if (allySpeed > topSpeed)
        {
            allySpeed -= enemyDecel;
        }

        // If swerving, speed up and run away.
        if ((target.transform.position - transform.position).magnitude < swerveDistance && allySpeed < topSpeed)
        {
            allySpeed += enemyAccel;
            swerving = true;
        }

        // If in pursuit but far away, speed up.
        if (!swerving && (target.transform.position - transform.position).magnitude > accelerateDistance && allySpeed < topSpeed)
        {
            allySpeed += enemyAccel;
        }

        // If close enough and in pursuit, decelerate.
        if (!swerving && (target.transform.position - transform.position).magnitude < decelerateDistance)
        {
            allySpeed -= enemyDecel;
        }

    }

    void moveForward()
    {
        transform.Translate(Vector3.up * Time.deltaTime * allySpeed);
    }


    public void DebuffSpeed(float amount)
    {
        allySpeed -= amount;
        if (allySpeed < 0)
        {
            allySpeed = 0;
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
            var part1 = target.transform.position;
            var part2 = target.transform.up;
            var part3 = target.GetComponent<EnemyMovement>().enemySpeed;
            targetPosition = (part1 + part2 * part3);
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
        //Debug.Log((transform.position - player.transform.position).magnitude);
        //Debug.Log(Vector3.Angle(transform.up, transform.position - player.transform.position) - 180);
        var newRotation = Quaternion.LookRotation(transform.position - targetPosition, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }
    #endregion
}
