using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float jumpSpeed = 100.0f;
    private float xBound = 45.0f;
    public float zBound = 86.0f;
    private Vector3 startPos;
    [SerializeField] Vector3 offset;
    private Rigidbody playerRb;
    private SpawnManager spawnManager;
    private Animator playerAnim;
    private int powerupIndex;
    private AudioSource playerAudio;
    private GameObject spine;

    public Camera cam;
    public Joystick joystickRight;
    public Joystick joystickLeft;
    public GameObject focalPoint;
    public GameObject[] powerupEffect;
    public ParticleSystem DeathEffect;
    public AudioClip crash;
    public AudioClip onCrossing;
    public AudioClip gainedPowerup;
    public GameObject[] Sensors;
    
    public bool hasPowerup;
    public bool isOnGround;
    public bool isDead;
    public bool hasJump;
    public bool hasSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startPos = new Vector3(0,0.5f,-13);
        offset = new Vector3 (0,2.8f,0);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
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
            float verticalInputRight = joystickRight.Vertical;
            
            float horizontalInputLeft = joystickLeft.Horizontal;
           
            float speed = 0;
            float runspeed = 1f;
            
            if(verticalInputRight > 0.25f)
            speed = runspeed;
            else if(verticalInputRight < -0.25f)
            speed = -runspeed;
            
            playerAnim.SetFloat("Speed_f",speed);
           
            cam.transform.Rotate(Vector3.up * horizontalInputLeft);
            transform.Rotate(Vector3.up * horizontalInputLeft);
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
            if(other.gameObject.name == "Powerup Jump(Clone)")
            {
                hasJump = true;
                powerupIndex = 0;
            }
            else
            {
                hasSpeed = true;
                powerupIndex = 1;
            }
            powerupEffect[powerupIndex].gameObject.SetActive(true);
            playerAudio.PlayOneShot(gainedPowerup,1.0f);
            powerupEffect[powerupIndex].transform.position = transform.position + offset;
            StartCoroutine(PowerupCountDownTimer(powerupIndex));
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Sensor"))
        {
            if(other.gameObject.name == "Sensor")
            {
                other.gameObject.SetActive(false);
                Sensors[3].gameObject.SetActive(true);
            }
            else if(other.gameObject.name == "Sensor (1)")
            {
                other.gameObject.SetActive(false);
                Sensors[0].gameObject.SetActive(true);
            }
            else if(other.gameObject.name == "Sensor (2)")
            {
                other.gameObject.SetActive(false);
                Sensors[1].gameObject.SetActive(true);
            }
            else if(other.gameObject.name == "Sensor (3)")
            {
                other.gameObject.SetActive(false);
                Sensors[2].gameObject.SetActive(true);
            }
            spawnManager.UpdateScore(1);
            playerAudio.PlayOneShot(onCrossing,1.0f);
        }
    }

    void OnPowerup()
    {
        if(hasPowerup && hasJump && isOnGround && spawnManager.isGameActive)
        {
            playerRb.AddForce(Vector3.up * jumpSpeed ,ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetBool("Jump_b",true);
        }
        if(hasPowerup && hasSpeed && isOnGround && spawnManager.isGameActive)
        {
            playerAnim.speed = 2.5f;
        }
        else
        {
            playerAnim.speed = 1.25f;
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
            playerAudio.PlayOneShot(crash,1.0f);
            powerupEffect[powerupIndex].gameObject.SetActive(false);
            isDead = true;
            DeathEffect.Play();
            playerAnim.SetBool("Death_b",true);
            spawnManager.GameOver();
        }
    }

    IEnumerator PowerupCountDownTimer(int index)
    {
        yield return new WaitForSeconds(20f);
        hasPowerup = false;
        powerupEffect[index].gameObject.SetActive(false);
    }
}
