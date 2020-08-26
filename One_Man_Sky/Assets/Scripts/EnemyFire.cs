using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    #region player_variables
    GameObject player;
    #endregion

    #region shooting_variables
    public bool openFire = false;
    [Tooltip("This is the minimum angle at which the enemy fires at a target.")]
    public float fireAngle = 30f;

    [Tooltip("This is how fast the shooter can shoot")]
    public float fireDelay;

    [Tooltip("This is the bullet this shooter is shooting")]
    public GameObject projectilePrefab;
    #endregion

    #region unity_functions
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        checkFire();
    }
    #endregion

    #region shooting_functions
    void checkFire()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.up);
        if (Vector3.Distance(player.transform.position, this.transform.position) < 60 && Mathf.Abs(angle) < fireAngle)
        {
            openFire = true;
        } else
        {
            openFire = false;
        }
    }
    #endregion
}
