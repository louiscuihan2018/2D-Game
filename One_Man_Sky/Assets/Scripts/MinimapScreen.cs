using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScreen : MonoBehaviour
{
    #region minimap_variable
    public GameObject minimap;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            toggleMinimap();
        }
    }
    #endregion

    #region minimap_functions
    void toggleMinimap()
    {
        minimap.SetActive(!minimap.activeInHierarchy);
    }
    #endregion
}
