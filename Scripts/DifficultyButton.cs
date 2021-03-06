﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private SpawnManager gameManager;

    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();  
        gameManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        button.onClick.AddListener(SetDifficulty); 
    }

    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name);
        gameManager.StartGame(difficulty);
    }
}
