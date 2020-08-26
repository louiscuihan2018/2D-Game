using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region slot_variables
    EquipmentManager equipmentManager;
    public Image icon;
    public Button removeButton;
    public Item item;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = EquipmentManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
    }
    #endregion

    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.enabled = true;
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        removeButton.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        icon.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);
    }

    public void getRidOfMyItem()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }

    }
}
