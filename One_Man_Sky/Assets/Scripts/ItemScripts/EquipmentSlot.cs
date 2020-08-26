using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    #region slot_variables
    public Image icon;
    public Equipment item;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    public void AddItem (Equipment newItem)
    {
        item = newItem;
        icon.enabled = true;
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
