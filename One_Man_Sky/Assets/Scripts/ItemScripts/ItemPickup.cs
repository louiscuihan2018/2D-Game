using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    #region item_variables
    public Item item;
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

    #region pickup_function
    public void pickUp()
    {
        if (Inventory.instance.autoEquip(item))
        {
            Destroy(this.gameObject);
            return;
        } else if (Inventory.instance.addItem(item))
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion
}
