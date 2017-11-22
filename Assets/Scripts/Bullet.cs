using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {

    private int bulletLife = 3;
   
    private void Start()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerTag"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
        }
        if (other.gameObject.CompareTag("WallTag"))
        {
            Destroy(gameObject);
        }
    }
    private void ShootfromEnemy(Vector2 direction)
    {
        //Rigidbody2D rbbul = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
    }
}
