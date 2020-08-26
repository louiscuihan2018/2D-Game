using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    #region item_variables
    public string itemName = "Untitled";
    public GameObject itemObject;
    public Sprite icon = null;
    public string description = "No description.";
    #endregion

    #region item_functions
    public virtual void Use()
    {
    }
    #endregion
}
