using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesLeft;
    public GameObject[] enemiesRight;
    public GameObject powerup; 
    public bool isGameActive;

    private float spawnYpos = 0.75f;
    private float spawnEnemyPosZ = 10.0f;
    private float spawnEnemyPosX = 25.0f;

    private float spawnEnemyPosZRight = 15.0f;
    private float spawnEnemyPosZRightEnd = 34.0f;

    private float spawnPowerupPosX = 20.0f;

    private float startDelay = 3.0f;
    private float spawnEnemyInterval = 5.0f;
    private float spawnPowerupInterval = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        InvokeRepeating("SpawnRandomEnemies",startDelay,spawnEnemyInterval);
        InvokeRepeating("SpawnEnemiesRight",startDelay,spawnEnemyInterval);
        InvokeRepeating("SpawnPowerup",startDelay,spawnPowerupInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemiesRight()
    {
        if(isGameActive)
        {
            float RandomPosZ = Random.Range(spawnEnemyPosZRight,spawnEnemyPosZRightEnd);
            int randomIndex = Random.Range(0,enemiesRight.Length);

            Vector3 spawnPos = new Vector3(-spawnEnemyPosX,spawnYpos,RandomPosZ);
           
            Instantiate(enemiesRight[randomIndex],spawnPos,enemiesRight[randomIndex].transform.rotation);
        }
    }
    void SpawnRandomEnemies()
    {
        if(isGameActive)
        {
            float RandomPosZ = Random.Range(spawnEnemyPosZ,-spawnEnemyPosZ);
            float RandomPosZodd = Random.Range(spawnEnemyPosZ*4,spawnEnemyPosZ * 5);
            
            int randomIndex = Random.Range(0,enemiesLeft.Length);

            Vector3 spawnPos = new Vector3(spawnEnemyPosX,spawnYpos,RandomPosZ);
            Vector3 spawnPosOdd = new Vector3(spawnEnemyPosX,spawnYpos,RandomPosZodd);


            Instantiate(enemiesLeft[randomIndex],spawnPos,enemiesLeft[randomIndex].transform.rotation);
            Instantiate(enemiesLeft[randomIndex],spawnPosOdd,enemiesLeft[randomIndex].transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        if(isGameActive)
        {
            float RandomPosX = Random.Range(spawnPowerupPosX,-spawnPowerupPosX);
            float RandomPosZ = Random.Range(spawnEnemyPosZ,spawnPowerupPosX*3);
            
            Vector3 spawnPos = new Vector3(RandomPosX,spawnYpos,RandomPosZ);
           if(GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
           {
               Instantiate(powerup,spawnPos,powerup.transform.rotation);
           } 
        }
    }
}
