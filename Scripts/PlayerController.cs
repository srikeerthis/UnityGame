using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 4500.0f;
    private float xBound = 40.0f;
    private float zBound = 60.0f;
    private Vector3 startPos;
    private Rigidbody playerRb;
    private SpawnManager spawnManager;
    private Animator playerAnim;

    public GameObject focalPoint;
    public ParticleSystem powerupEffect;
    public bool hasPowerup;
    public bool isOnGround;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startPos = transform.position;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        BoundaryConstraints();
        OnPowerup();
    }

    // Move player based on arrow keys
    void MovePlayer()
    {
        if(spawnManager.isGameActive)
        {
            playerAnim.SetFloat("Speed_f",0.5f);
            // float mouseX = Input.GetAxis("Mouse X");  
            // transform.Rotate(Vector3.up * mouseX);
            
            float verticalInput = Input.GetAxis("Vertical");  
            playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        }   
    }

    // Make player move within a boundary
    void BoundaryConstraints()
    {
        if(transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound,transform.position.y,transform.position.z);
        }

        if(transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound,transform.position.y,transform.position.z);
        }

        if(transform.position.z > zBound)
        {
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupEffect.Play();
            StartCoroutine(PowerupCountDownTimer());
            Destroy(other.gameObject);
        }
    }

    void OnPowerup()
    {
        if(hasPowerup && Input.GetKeyDown(KeyCode.Space) && isOnGround && spawnManager.isGameActive)
        {
            playerRb.AddForce(Vector3.up * speed * Time.deltaTime,ForceMode.Impulse);
            playerAnim.SetBool("Jump_b",true);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround= true;
            playerAnim.SetBool("Jump_b",false);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            spawnManager.isGameActive = false;
            playerAnim.SetBool("Death_b",true);            
        }
    }

    IEnumerator PowerupCountDownTimer()
    {
        yield return new WaitForSeconds(10f);
        hasPowerup = false;
        powerupEffect.Stop();
    }
}
