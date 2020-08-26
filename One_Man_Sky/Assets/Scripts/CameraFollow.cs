using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region player_variables
    [Tooltip("This is the gameobject to be followed. (Default: player GameObject)")]
    public GameObject player;

    Rigidbody2D playerRB;
    #endregion

    #region relevant_camera_variables
    [Tooltip("This is how responsive the camera is to player position. Lower is closer, higher is looser. (Default: 5)")]
    public float smoothFactor = 5;

    [Tooltip("0 is the old camera position, 1 is the new camera position. (Default: 1)")]
    public int cameraControlType = 1;

    float smoothSpeed = 0;
    
    Vector3 offset;
    Vector3 velocity = Vector3.zero;
    #endregion

    #region unity_functions
    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();

        //The offset is the height of the camera relative to the player. If we don't add the offset, our camera will be brought down to the player's depth (z).
        offset = transform.position - player.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && cameraControlType == 0)
        {
            cameraControlType = 1;
        }
        else if (Input.GetKeyDown(KeyCode.V) && cameraControlType == 1)
        {
            cameraControlType = 0;
        }

        if (player.GetComponent<PlayerMovement>().currentSpeed != 0)
        {
            transform.position = updateCameraPosition();
        }
    }
    private void LateUpdate()
    {
        //This camera follow speed is proportional to how far the player is from the camera (x, y). The further they are, the faster the camera moves to them.
        smoothSpeed = smoothFactor/((transform.position - offset - player.transform.position).magnitude + 1);
    }
    #endregion

    #region camera_functions
    Vector3 updateCameraPosition()
    {
        if (cameraControlType == 0)
        {
            return cameraType0();
        }
        else if (cameraControlType == 1)
        {
            return cameraType1();
        }
        return cameraType0();
    }

    Vector3 cameraType0()
    {
        Vector3 playerDestination = player.transform.position + player.transform.up * Mathf.Sqrt(player.GetComponent<PlayerMovement>().currentSpeed) * 2;
        Vector3 desiredPosition = playerDestination + offset;

        //This vector is similar to how Lerping (linearly interpolating) works, but avoids the jittering that occurs.
        return Vector3.SmoothDamp(desiredPosition, transform.position, ref velocity, smoothSpeed);
    }

    Vector3 cameraType1()
    {
        Vector3 playerDestination = player.transform.position;
        Vector3 desiredPosition = playerDestination + offset;

        return Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
    #endregion

    #region camera_coroutines
    IEnumerator camera_transition()
    {
        float timer = 0;
        while (timer < 1f)
        {
            transform.position = Vector3.SmoothDamp(updateCameraPosition(), transform.position, ref velocity, smoothSpeed);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //No longer transitioning.
    }
    #endregion
}
