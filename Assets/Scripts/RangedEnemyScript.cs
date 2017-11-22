using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyScript : BaseUnit
{

    private int direction = 1;
    
    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");

        /* if (!IsGrounded(raycastOffset))
         {
             direction = -1;
         }
         if (!IsGrounded(-raycastOffset))
         {
             direction = 1;
         }*/
        Move(direction, 0);


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            direction *= -1;
        }
        if (other.gameObject.CompareTag("WallTag"))
        {
            direction *= -1;
        }
     
    }
  
}

