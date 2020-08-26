using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    #region pause_menu_variables
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public GameObject controlsUI;
    #endregion

    #region pause_variables
    public bool paused = false;
    public GameObject pauseScreen;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !paused)
        {
            pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused)
        {
            unpause();
        }
    }
    #endregion

    #region pause_menu
    public void toggleControls()
    {
        controlsUI.SetActive(!controlsUI.activeInHierarchy);
    }

    public void toggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        equipmentUI.SetActive(!equipmentUI.activeInHierarchy);
        controlsUI.SetActive(false);
    }
    #endregion

    #region pause_functions
    public void pause()
    {
        Time.timeScale = 0.00001f;
        paused = true;
        pauseScreen.SetActive(true);
    }

    public void unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pauseScreen.SetActive(false);
        controlsUI.SetActive(false);
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        GameObject.Find("Player").gameObject.GetComponent<PlayerEquipment>().synchronizeAll();
    }
    #endregion
}
