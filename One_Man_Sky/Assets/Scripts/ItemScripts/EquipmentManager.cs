using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region equipment_variables
    public static EquipmentManager instance;

    public Transform equipmentParent;
    public Equipment[] currentEquipment;
    public EquipmentSlot[] equipmentSlots;
    public bool hasUpdated;
    #endregion

    #region unity_functions
    void Awake()
    {
        instance = this;
        int numSlots =  System.Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
        hasUpdated = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    public void toggleQuip(Equipment newItem)
    {
        int slotIndex = -1;
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if (currentEquipment[i] == newItem)
            {
                slotIndex = i;
                break;
            }
        }
        if (slotIndex != -1 && newItem != null)
        {
            unequip(slotIndex);
        } else
        {
            equip(newItem);
        }
        hasUpdated = true;
    }
    public void equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldEquipment = null;

        Inventory.instance.Remove(newItem);
        if (currentEquipment[slotIndex] != null)
        {
            oldEquipment = currentEquipment[slotIndex];
            Inventory.instance.addItem(oldEquipment);
        }
        currentEquipment[slotIndex] = newItem;
        equipmentSlots[slotIndex].AddItem(newItem);
    }

    public bool autoEquip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        if (currentEquipment[slotIndex] != null)
        {
            return false;
        }
        currentEquipment[slotIndex] = newItem;
        equipmentSlots[slotIndex].AddItem(newItem);
        hasUpdated = true;
        return true;
    }

    public void unequip (int slotIndex)
    {
        Equipment oldItem = currentEquipment[slotIndex];
        equipmentSlots[slotIndex].ClearSlot();
        Inventory.instance.addItem(oldItem);

        currentEquipment[slotIndex] = null;
    }
}
