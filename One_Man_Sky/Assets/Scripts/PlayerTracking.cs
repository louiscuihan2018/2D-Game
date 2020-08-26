using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    #region player_variables
    public GameObject player;
    Vector3 targetAngle;
    #endregion

    #region turning_variables
    public int targetingType = 2;
    public float rotateSpeed = 3;
    Vector3 currentAngle;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (targetingType == 0)
        {
            trackPlayer();
        } else if (targetingType == 1)
        {
            noisyTrackPlayer();
        } else if (targetingType == 2)
        {
            advancedTrackPlayer();
        } else if (targetingType == 3)
        {
            noisyAdvancedTrackPlayer();
        }
        
    }
    #endregion

    #region turning_functions
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
        Vector3 playerDestination = player.transform.position + player.transform.up * player.GetComponent<PlayerMovement>().currentSpeed * 1.5f;
        var newRotation = Quaternion.LookRotation(transform.position - playerDestination, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }

    void noisyAdvancedTrackPlayer()
    {

        Vector3 playerDestination = player.transform.position + player.transform.up * player.GetComponent<PlayerMovement>().currentSpeed;
        Vector3 noisyDestination = playerDestination + new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);

        var newRotation = Quaternion.LookRotation(transform.position - noisyDestination, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
    }
    #endregion
}
