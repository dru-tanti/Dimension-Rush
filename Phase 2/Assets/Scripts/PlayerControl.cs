using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int speed = 10;
    private bool facingRight = false;
    public int jump = 10;

    private bool grounded = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private Animator anim;
    
    private float moveX;

    private Rigidbody2D _playerRB;

    // Retrieves the players rigidbody so that we can move it.
    private void Awake() 
    {
        anim = GetComponent<Animator>();
        _playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        // Debug.Log(grounded);

        if(Input.GetKey(KeyCode.P))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
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
        moveX = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsRunning", true);
        } else {
            anim.SetBool("IsRunning", false);
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

    // Inverts the players scale to make it look as if they are moving left and right.
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale  = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            anim.SetTrigger("TakeOff");
            isJumping = true;
            jumpTimeCounter = jumpTime;

            // Applies force when the player presses the Jump Button.
            _playerRB.velocity = Vector2.up * jump;   

        }

        if(grounded == true)
        {
            anim.SetBool("IsJumping", false);
        } else {
            anim.SetBool("IsJumping", true);
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0) {
                // Applies force when the player presses the Jump Button.
                _playerRB.velocity = Vector2.up * jump;   
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        // When the space key is released , disable the 
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.tag == "Pit")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
