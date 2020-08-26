using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zoomDelta = Input.mouseScrollDelta.y;
        Camera.main.orthographicSize -= zoomDelta;
        if (Input.GetKey(KeyCode.PageUp))
        {
            Camera.main.orthographicSize -= 0.1f;
        } else if (Input.GetKey(KeyCode.PageDown))
        {
            Camera.main.orthographicSize += 0.1f;
        }
    }
    #endregion
}
