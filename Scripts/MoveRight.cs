using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 5.0f;

    private float xDestroy = 43.0f;
    private Rigidbody objectRb;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();   
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnManager.isGameActive)
        {
            objectRb.AddForce(Vector3.right * speed);

            if(transform.position.x >  xDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
