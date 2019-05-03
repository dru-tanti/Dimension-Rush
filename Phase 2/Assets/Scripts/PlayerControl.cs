using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int speed = 10;
    private bool facingRight = false;
    public int jump = 2000;
    private float moveX;

    private TimeTravel _time;

    private Rigidbody2D _playerRB;

    private void Start() 
    {
    
    }

    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Submit"))
        {
            Jump();
        }

        if (moveX < 0f && facingRight == false)
        {
            FlipPlayer();
        } else if (moveX > 0f && facingRight == true) 
        {
            FlipPlayer();
        }

        _playerRB.velocity = new Vector2 (moveX * speed, _playerRB.velocity.y);
    }

    void Jump()
    {
        _playerRB.AddForce(Vector2.up * jump);   
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale  = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
