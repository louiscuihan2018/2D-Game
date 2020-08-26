using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region inventory_variables
    public static Inventory instance;

    public List<Item> items = new List<Item>();
    public int numberOfItems = 0;
    public int capacity = 24;

    public Transform itemsParent;
    public Transform equipmentParent;

    public InventorySlot[] slots;
    bool inventoryUIActive = false;
    #endregion

    #region tooltip_variables
    public GameObject tooltip;
    public Text nameAndClass;
    public Text description;
    public Image icon;
    #endregion

    #region unity_functions
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("There's more than one Inventory!");
        }
        instance = this;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !inventoryUIActive)
        {
            enableInventoryUI();
            inventoryUIActive = true;
        } else if (Input.GetKeyDown(KeyCode.I) && inventoryUIActive)
        {
            disableInventoryUI();
            inventoryUIActive = false;
        }
    }
    #endregion

    #region inventory_functions
    public bool autoEquip(Item item)
    {
        if (item is Equipment)
        {
            return EquipmentManager.instance.autoEquip((Equipment)item);
        } else
        {
            return this.addItem(item);
        }
    }

    public bool addItem(Item item)
    {
        if (numberOfItems < capacity)
        {
            items.Add(item);
            numberOfItems += 1;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void Remove(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            if (getIndexOfItem(item) != -1)
            {
                slots[getIndexOfItem(item)].ClearSlot();
            }
            numberOfItems -= 1;
            UpdateUI();
        }
    }

    public int getIndexOfItem(Item item)
    {
        if (!items.Contains(item))
        {
            return -1;
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                return i;
            }
        }
        return -1;
    }
    #endregion

    #region inventoryUI_functions
    void enableInventoryUI()
    {
        this.GetComponentInParent<PauseScreen>().pause();
        this.GetComponentInParent<PauseScreen>().toggleInventory();
    }

    void disableInventoryUI()
    {
        this.GetComponentInParent<PauseScreen>().unpause();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < numberOfItems)
            {
                slots[i].AddItem(instance.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
    #endregion
}
