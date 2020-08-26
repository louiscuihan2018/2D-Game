using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region player_variables
    public GameObject player;
    #endregion

    #region fixed_movement_variables
    float enemyDecel = 0.5f;
    float enemyAccel = 1;
    #endregion

    #region movement_variables
    public float enemySpeed = 1f;
    float topSpeed = 45f;
    bool swerving = false;
    #endregion

    #region turning_variables
    public int targetingType = 2;
    public float rotateSpeed = 3;
    Vector3 currentAngle;
    float swerveTimer;
    float swerveDuration = 1f;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        swerveTimer = swerveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        setSpeed();
        moveForward();
        turn();
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
        if ((player.transform.position - transform.position).magnitude < 20)
        {
            enemySpeed += enemyAccel;
            swerving = true;
        }

        // If in pursuit but far away, speed up.
        if (!swerving && (player.transform.position - transform.position).magnitude > 80)
        {
            enemySpeed += enemyAccel;
        }

        // If close enough and in pursuit, decelerate.
        if (!swerving && (player.transform.position - transform.position).magnitude < 40)
        {
            enemySpeed -= enemyDecel;
        }
        
    }

    void moveForward()
    {
        transform.Translate(Vector3.up * Time.deltaTime * enemySpeed);
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
            trackPlayer();
        }
        else if (targetingType == 1)
        {
            noisyTrackPlayer();
        }
        else if (targetingType == 2)
        {
            advancedTrackPlayer();
        }
        else if (targetingType == 3)
        {
            noisyAdvancedTrackPlayer();
        }
    }

    void swerve()
    {
        if (swerveTimer < 0)
        {
            swerving = false;
            swerveTimer = swerveDuration;
        }
        var newRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
        swerveTimer -= Time.deltaTime;
    }

    void trackPlayer()
    {
        //Debug.Log((transform.position - player.transform.position).magnitude);
        //Debug.Log(Vector3.Angle(transform.up, transform.position - player.transform.position) - 180);
        var newRotation = Quaternion.LookRotation(transform.position - player.transform.position, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }

    void noisyTrackPlayer()
    {
        Vector3 noisyDestination = player.transform.position + new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

        var newRotation = Quaternion.LookRotation(transform.position - noisyDestination, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }

    void advancedTrackPlayer()
    {
        Vector3 playerDestination = player.transform.position + player.transform.up * player.GetComponent<PlayerMovement2>().currentSpeed * 1.5f;
        var newRotation = Quaternion.LookRotation(transform.position - playerDestination, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }

    void noisyAdvancedTrackPlayer()
    {

        Vector3 playerDestination = player.transform.position + player.transform.up * player.GetComponent<PlayerMovement2>().currentSpeed;
        Vector3 noisyDestination = playerDestination + new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

        var newRotation = Quaternion.LookRotation(transform.position - noisyDestination, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }
    #endregion
}
