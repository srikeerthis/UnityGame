using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesLeft;
    public GameObject[] enemiesRight;
    public GameObject[] powerup;
    public GameObject[] trees; 
    public Button mainMenu;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject instructionScreen;
    public GameObject pauseInstructionScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI ScoreText;
    public Text gameOverText;
    public Button restartGame;

    private PlayerController player;
    private int score;
    private float spawnYpos = 1f;
    private float spawnEnemyPosZ = 8.0f;
    private float spawnEnemyPosX = 50.0f;

    private float spawnEnemyPosZRight = 16.0f;
    private float spawnEnemyPosZRightEnd = 33.0f;

    private float spawnPowerupPosX = 20.0f;

    private float spawnEnemyInterval = 6.0f;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void StartGame(int difficulty)
    {
        for(int i =0 ;i < trees.Length;i++)
        trees[i].gameObject.SetActive(true);

        mainMenu.gameObject.SetActive(true);
        player.joystickRight.gameObject.SetActive(true);
        player.joystickLeft.gameObject.SetActive(true);

        ScoreText.gameObject.SetActive(true);
        score = 0;
        spawnEnemyInterval /= difficulty;

        isGameActive = true;   
        
        StartCoroutine(SpawnPowerup(spawnEnemyInterval));
        StartCoroutine(SpawnEnemiesRight(spawnEnemyInterval));
        StartCoroutine(SpawnRandomEnemies(spawnEnemyInterval));
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Roads Crossed : " + score; 
    }

    public void GameOver()
    {
        isGameActive = false;
        mainMenu.gameObject.SetActive(false);
        
        gameOverText.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true);            
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        
        player.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(true);

        mainMenu.gameObject.SetActive(false);
        player.joystickLeft.gameObject.SetActive(false);
        player.joystickRight.gameObject.SetActive(false);
        pauseInstructionScreen.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        isGameActive = true;
        for(int i =0 ;i < trees.Length;i++)
        trees[i].gameObject.SetActive(true);

        Time.timeScale = 1.0f;
        
        mainMenu.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(true);
        player.joystickLeft.gameObject.SetActive(true);
        player.joystickRight.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        
        pauseScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
        instructionScreen.gameObject.SetActive(false);
    }

    public void StartScreen()
    {
        player.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(true);
        
        isGameActive = false;
        pauseScreen.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        instructionScreen.gameObject.SetActive(false);
    }

    public void Instructions()
    {
        instructionScreen.gameObject.SetActive(true);
        
        for(int i =0 ;i < trees.Length;i++)
        trees[i].gameObject.SetActive(false);
       
        ScoreText.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
        
    }

    public void PauseInstructions()
    {
        pauseInstructionScreen.gameObject.SetActive(true);
        
        for(int i =0 ;i < trees.Length;i++)
        trees[i].gameObject.SetActive(false);
       
        ScoreText.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
        
    }

    public void ExitGame()
    {
        Application.Quit();
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
