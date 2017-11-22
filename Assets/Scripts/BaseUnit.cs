using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
//[RequireComponent(typeof(Animator))]
public class BaseUnit : MonoBehaviour
{
    public float moveSpeed = 5;
    public float raycastOffset = 0.4f;
    public float raycastDistance = 0.1f;


    protected Rigidbody2D _RB;
    protected SpriteRenderer _SR;
  //  protected Animator _Anim;

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        
        _SR = GetComponent<SpriteRenderer>();
      //  _Anim = GetComponent<Animator>();
    }


    protected void Move(float direction, int speed)
    {
        if (direction < 0) _SR.flipX = true;
        else if (direction > 0) _SR.flipX = false;
        _RB.velocity = new Vector2(speed * direction, _RB.velocity.y);
        
       // _Anim.SetFloat("MoveSpeed", Mathf.Abs(direction));
    }

    protected bool IsGrounded(float offsetX)
    {
        Vector2 origin = transform.position;
        origin.x += offsetX;

        Debug.DrawRay(origin, Vector3.down);

        RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.down, raycastDistance);

        if (hitInfo.collider == null)
        {
            transform.SetParent(null);
            return (false);
        }
        /*if (hitInfo.collider.GetComponent<MovingPlatform>() != null)
        {
            transform.SetParent(hitInfo.transform);
        }*/

        return hitInfo;
    }
}
