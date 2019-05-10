using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int speed = 10;
    private bool facingRight = false;

    private bool grounded = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    
    public int jump = 2000;
    private float moveX;

    private Rigidbody2D _playerRB;

    // Retrieves the players rigidbody so that we can move it.
    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(grounded);
    }

    private void FixedUpdate() 
    {
        // Will check if the character is touching the ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Move();
    }

    // Controls the movement of the player.
    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        // Inverts the player model if they are moving to the left.
        if (moveX < 0f && facingRight == false)
        {
            FlipPlayer();
        } else if (moveX > 0f && facingRight == true) 
        {
            FlipPlayer();
        }

        // Moves the players rigidbody.
        _playerRB.velocity = new Vector2 (moveX * speed, _playerRB.velocity.y);
    }

    // Applies force when the player presses the Jump Button.
    void Jump()
    {
        _playerRB.AddForce(Vector2.up * jump);   
    }
    
    // Inverts the players scale to make it look as if they are moving left and right.
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale  = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
