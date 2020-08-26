using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    #region loot_variables
    public static LootTable instance;
    public GameObject[] lootTable;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region loot_functions
    public GameObject getLoot()
    {
        GameObject loot = null;
        if (lootTable != null && lootTable.Length > 0)
        {
            int lootIndex = Random.Range(0, lootTable.Length - 1);
            loot = lootTable[lootIndex];
        }
        return loot;
    }
    #endregion
}
