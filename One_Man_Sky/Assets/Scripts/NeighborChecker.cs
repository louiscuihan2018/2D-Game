using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborChecker : MonoBehaviour
{
    #region neighbor_variables
    List<GameObject> neighborList;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        neighborList = this.GetComponentInParent<EnemyMovement2>().neighbors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region neighbor_functions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            neighborList.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            neighborList.Remove(collision.gameObject);
        }
    }
    #endregion
}
