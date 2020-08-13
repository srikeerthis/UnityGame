using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset;
    private PlayerController player;
    private SpawnManager spawnManager; 

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        offset = new Vector3(0,2.5f,0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(spawnManager.isGameActive)
        {
            transform.position = player.transform.position + offset;
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
