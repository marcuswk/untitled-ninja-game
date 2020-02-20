using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerUps;

    private float waitToSpawn = 2;
    private float timeBetweenSpawns = 2;
    // Start is called before the first frame update
    void Start()
    {
      //  InvokeRepeating("spawnRandomEnemy", waitToSpawn, timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemyOnX();
        SpawnPowerUpOnZ();
        KillAllEnemiesOnK();
    }

    void spawnRandomEnemy()
    {
        Vector3 randomSpawnLocation = new Vector3(Random.Range(-15, 15), 1, Random.Range(-9, 9));
        int randomEnemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomEnemy], randomSpawnLocation, enemies[randomEnemy].transform.rotation);
    }



    //spawn enemies on press of X
    void SpawnEnemyOnX()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Vector3 randomSpawnLocation = new Vector3(Random.Range(-15, 15), 1, Random.Range(-9, 9));
            Vector3 randomSpawnLocation = new Vector3(1.2f, 1f, 3.5f);
            int randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], randomSpawnLocation, enemies[randomEnemy].transform.rotation);
        }
    }
    //spawn powerups on press of Z
    void SpawnPowerUpOnZ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 randomSpawnLocation = new Vector3(Random.Range(-15, 15), 1, Random.Range(-9, 9));
            int randomPowerUp = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[randomPowerUp], randomSpawnLocation, enemies[randomPowerUp].transform.rotation);
        }
    }
    
    //Kill Everyone!
    void KillAllEnemiesOnK()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
        }
    }
    
}
