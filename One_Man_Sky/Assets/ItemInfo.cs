using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region information_variables
    private GameObject tooltip;
    private Text nameAndClass;
    private Text description;
    private Image iconFrame;

    private Item item;
    private Image icon;
    #endregion

    #region unity_functions
    void Start()
    {
        getTooltip();
    }
    void Update()
    {
        
    }
    #endregion

    #region interaction_functions
    private void getTooltip()
    {
        tooltip = Inventory.instance.tooltip;
        nameAndClass = Inventory.instance.nameAndClass;
        description = Inventory.instance.description;
        iconFrame = Inventory.instance.icon;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (tooltip == null)
        {
            getTooltip();
        }
        if (this.GetComponent<InventorySlot>() != null)
        {
            item = this.GetComponent<InventorySlot>().item;
            icon = this.GetComponent<InventorySlot>().icon;
        }
        if (this.GetComponent<EquipmentSlot>() != null)
        {
            item = this.GetComponent<EquipmentSlot>().item;
            icon = this.GetComponent<EquipmentSlot>().icon;
            
        }
        if (item != null)
        {
            tooltip.SetActive(true);
            nameAndClass.text = item.itemName;
            iconFrame.sprite = icon.sprite;
            description.text = "Description: " + item.description;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        tooltip.SetActive(false);
    }
    #endregion
}
