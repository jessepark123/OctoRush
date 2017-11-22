using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class PlayerInputs : BaseUnit
{



    public float jumpSpeed = 12;
    public float killHeight = -50;
    public int numberofJumps;
    public AudioClip coinSound;
    public AudioClip deathSound;
    private AudioSource _ASource;
    private int jumpCount;
    private Transform _Cam;
    public int bulletSpeed;
    public GameObject bullet;
    public GameObject bullet1;
    [SerializeField]
    [Range(1, 200)]
    private float blinkSpeed;
    private bool isBlinking;
    private bool isBlinkReady;
    [SerializeField]
    [Range(1, 10)]
    private float blinkCooldown;
    private int d;

    private void Start()
    {
       
        _Cam = Camera.main.transform;
        _ASource = GetComponent<AudioSource>();
        isBlinking = false;
        isBlinkReady = true;
        d = 1;
    }


    private void Update()
    {
        
        //Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        
        Move(horizontalInput, 5);

        //Ground Checking
        bool groundedTrue = IsGrounded(raycastOffset) || IsGrounded(-raycastOffset);
        if (groundedTrue) jumpCount = 0;
        // _Anim.SetBool("InAir", !groundedTrue);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(1f);
        }
/*
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();
        if (Input.GetMouseButtonDown(0))
        {
            // Creates the bullet locally
            GameObject bullet = (GameObject)Instantiate(bullet1, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

            // Adds velocity to the bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
        */
        //Blink
        if (Input.GetMouseButtonDown(1))
        {
            if (horizontalInput > 0.9 || horizontalInput < -0.9)
            {
                Blink();
            }
        }


        if (transform.position.y <= killHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    private void Blink()
    {
        if (d == 1)
        {
            _ASource.pitch = 1;
            _ASource.Play();
            StartCoroutine(BlinkCoroutine());
            
            d++;
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        if (isBlinkReady == true)
        {
            
            isBlinkReady = false;
            print("Blink is on cooldown");

           

            _RB.velocity = new Vector2(Input.GetAxis("Horizontal"), 0) * blinkSpeed;
            
            isBlinking = true;
            
            yield return new WaitForSecondsRealtime(1);
            isBlinking = false;
        }
        if (isBlinkReady == false)
        {
            yield return new WaitForSecondsRealtime(blinkCooldown);
            isBlinkReady = true;
            print("Blink is ready");
            d = 1;
        }
    }

    private void Die()
    {
       // StartCoroutine(DeathCoroutine());
    }

   /* IEnumerator DeathCoroutine()
    {
        _ASource.PlayOneShot(deathSound);
        _Cam.SetParent(null);
        transform.SetParent(null);
       // _Anim.SetBool("IsAlive", false);


        GetComponent<Collider2D>().enabled = false;
        Jump(2f);

        yield return new WaitForSecondsRealtime(2); //waits 2 secs


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield break; //exits coroutine
    }*/

    
    private void Jump(float jumpMult)
    {
        if (numberofJumps > jumpCount)
        {
            _RB.velocity = new Vector2(_RB.velocity.x, jumpSpeed);
            ++jumpCount;
        }


    }
   
       

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyTag"))
        {
            KillEnemy(other.gameObject);
        }

    }

    private void KillEnemy(GameObject enemy)
    {
        
        Destroy(enemy);
       // Globals.instance.killCount++;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("EnemyTag"))
        {
            if (isBlinking == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                KillEnemy(other.gameObject);
                _ASource.pitch += 2;
                _ASource.Play();
            }
        }
    }
}
