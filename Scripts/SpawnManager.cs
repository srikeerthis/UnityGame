using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesLeft;
    public GameObject[] enemiesRight;
    public GameObject[] powerup; 
    public bool isGameActive;
    public GameObject titleScreen;
    public Text tmptex;
    public Button restartGame;

    private float spawnYpos = 0.75f;
    private float spawnEnemyPosZ = 8.0f;
    private float spawnEnemyPosX = 50.0f;

    private float spawnEnemyPosZRight = 16.0f;
    private float spawnEnemyPosZRightEnd = 33.0f;

    private float spawnPowerupPosX = 20.0f;

    private float spawnEnemyInterval = 5.0f;
 
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        
        spawnEnemyInterval /= difficulty;

        StartCoroutine(SpawnPowerup(spawnEnemyInterval));
        StartCoroutine(SpawnEnemiesRight(spawnEnemyInterval));
        StartCoroutine(SpawnRandomEnemies(spawnEnemyInterval));

        titleScreen.gameObject.SetActive(false);
    }
    
    public void GameOver()
    {
        isGameActive = false;
        tmptex.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true);            
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnEnemiesRight(float spawnInterval)
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnInterval);
            float RandomPosZ = Random.Range(spawnEnemyPosZRight,spawnEnemyPosZRightEnd);
            float RandomPosZodd = Random.Range(spawnEnemyPosZRightEnd * 2  + 1,spawnEnemyPosZRight * 5 + 2);

            int randomIndex = Random.Range(0,enemiesRight.Length);

            Vector3 spawnPos = new Vector3(-spawnEnemyPosX,spawnYpos,RandomPosZ);
            Vector3 spawnPosOdd = new Vector3(-spawnEnemyPosX,spawnYpos,RandomPosZodd);

            Instantiate(enemiesRight[randomIndex],spawnPos,enemiesRight[randomIndex].transform.rotation);
            Instantiate(enemiesRight[randomIndex],spawnPosOdd,enemiesRight[randomIndex].transform.rotation);
        }
    }
    IEnumerator SpawnRandomEnemies(float spawnInterval)
    {
        
        while(isGameActive)
        {
           yield return new WaitForSeconds(spawnInterval);
            float RandomPosZ = Random.Range(spawnEnemyPosZ,-spawnEnemyPosZ);
            float RandomPosZodd = Random.Range(spawnEnemyPosZ*5 + 1,spawnEnemyPosZ * 7);
            
            int randomIndex = Random.Range(0,enemiesLeft.Length);

            Vector3 spawnPos = new Vector3(spawnEnemyPosX,spawnYpos,RandomPosZ);
            Vector3 spawnPosOdd = new Vector3(spawnEnemyPosX,spawnYpos,RandomPosZodd);


            Instantiate(enemiesLeft[randomIndex],spawnPos,enemiesLeft[randomIndex].transform.rotation);
            Instantiate(enemiesLeft[randomIndex],spawnPosOdd,enemiesLeft[randomIndex].transform.rotation);
        }
    }

    IEnumerator SpawnPowerup(float spawnInterval)
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnInterval);
            int randomIndex = Random.Range(0,powerup.Length);
            float RandomPosX = Random.Range(spawnPowerupPosX,-spawnPowerupPosX);
            float RandomPosZ = Random.Range(spawnEnemyPosZ,spawnPowerupPosX*3);
            
            Vector3 spawnPos = new Vector3(RandomPosX,spawnYpos,RandomPosZ);
           if(GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
           {
               Instantiate(powerup[randomIndex],spawnPos,powerup[randomIndex].transform.rotation);
           } 
        }
    }
}
