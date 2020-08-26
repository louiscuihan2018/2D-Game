using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    #region player_variables
    GameObject player;
    #endregion

    #region spawn_variables
    [Tooltip("The number of seconds between each enemy spawn.")]
    public float spawnInterval = 5f;

    float spawnTimer;
    public float minimumRange = 10f;
    public GameObject defaultEnemyShip = null;
    public GameObject[] ShipsToSpawn;
    public int forceSpawnIndex = 0;
    float escalationTimer = 0;
    #endregion

    #region unity_functions
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            spawnInterval -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            spawnInterval += 1;
        }
        if (spawnInterval < 1)
        {
            spawnInterval = 1;
        }
        minimumRange = Camera.main.orthographicSize + 20;
        escalationTimer += Time.deltaTime;
        if (escalationTimer > 30)
        {
            StartCoroutine(Spawn());
            escalationTimer = 0;
        }
    }
    #endregion

    #region spawn_functions
    private void forceSpawnUpdate()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            forceSpawnIndex = 1;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            forceSpawnIndex = 2;
        }
        if (Input.GetKey(KeyCode.F3))
        {
            forceSpawnIndex = 3;
        }
        if (Input.GetKey(KeyCode.F4))
        {
            forceSpawnIndex = 4;
        }
        if (Input.GetKey(KeyCode.F5))
        {
            forceSpawnIndex = 5;
        }
        if (Input.GetKey(KeyCode.F10))
        {
            forceSpawnIndex = 0;
        }
    }
    #endregion

    #region spawn_couroutine
    IEnumerator Spawn()
    {
        while (true)
        {
            Vector3 location = player.transform.position + new Vector3((Random.Range(0, 2) * 2 - 1)*Random.Range(minimumRange, minimumRange*1.5f), (Random.Range(0, 2) * 2 - 1) * Random.Range(minimumRange, minimumRange*1.5f), 0f);
            if (spawnTimer < 0)
            {
                GameObject toSpawn = defaultEnemyShip;
                if (ShipsToSpawn != null && ShipsToSpawn.Length > 0)
                {
                    int shipIndex = Random.Range(0, ShipsToSpawn.Length);
                    toSpawn = ShipsToSpawn[shipIndex];
                }
                if (forceSpawnIndex != 0 && forceSpawnIndex < ShipsToSpawn.Length - 1)
                {
                    toSpawn = ShipsToSpawn[forceSpawnIndex];
                }
                if (toSpawn != null || (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Ally").Length) < 25)
                {
                    Instantiate(toSpawn, location, Quaternion.identity);
                    spawnTimer = spawnInterval;
                }
            }
            spawnTimer -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
