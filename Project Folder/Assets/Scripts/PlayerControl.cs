﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))] [RequireComponent(typeof(TimeTravel))]
public class PlayerControl : MonoBehaviour
{
    [Header("Movement Variables")]
    public int speed = 10;
    private bool facingRight = false;
    public int jump = 10;

    [Header("Jump Variables")]
    private bool grounded = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public bool isDead;

    [Header("Animation Controllers")]
    public ParticleSystem landDust;
    private bool spawnDust;
    private Animator anim;
    private static TimeTravel time;
    private float moveX;
    private Rigidbody2D _playerRB;
	public UnityAction PlayerDeath;
	public UnityAction LevelComplete;

    // Retrieves the players rigidbody so that we can move it.
    private void Awake() 
    {
        GameObject GameManager = GameObject.Find("GameManager");
        time = GameManager.GetComponent<TimeTravel>();

        anim = GetComponent<Animator>();   

        _playerRB = GetComponent<Rigidbody2D>();
        isDead = false;
    }

    void Update()
    {
        if(!isDead)
        {
            Jump();
        }
        // Debug.Log(grounded);
    }

    private void FixedUpdate() 
    {
        // Adjusts whatIsGround depending on what layer the player is meant to interact with
        if(time.inPast)
        {
            whatIsGround = LayerMask.GetMask("Present");
        } else {
            whatIsGround = LayerMask.GetMask("Past");
        }

        // Will check if the character is touching the ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // Disables movement if the player dies
        if(!isDead)
        {
            Move();
        }
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
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            anim.SetTrigger("TakeOff");
            isJumping = true;
            jumpTimeCounter = jumpTime;

            // Applies force when the player presses the Jump Button.
            _playerRB.velocity = Vector2.up * jump;   
        }

        // Play landing animation and spawn dust when the player becomes grounded again
        if(!grounded)
        {
            anim.SetBool("IsJumping", true);
            spawnDust = true;
        } else {
            anim.SetBool("IsJumping", false);
            if(spawnDust == true)
            {
                landDust.Play();
                AudioManager.current.Play("Landing");
                CameraShaker.Instance.ShakeOnce(1.3f, 2f, 0.1f, 1f);
                spawnDust = false;
            }
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetKey(KeyCode.W) && isJumping == true)
        {
            if(jumpTimeCounter > 0) {
                // Applies force when the player presses the Jump Button.
                _playerRB.velocity = Vector2.up * jump;   
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        // When the space key is released, disable the 
        if(Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }

    // Function that is called by the player animation controller.
    void Run()
    {
        // Adjusts whatIsGround depending on what layer the player is meant to interact with
        if(time.inPast)
        {
            AudioManager.current.Play("Run2");
        } else {
            AudioManager.current.Play("Run");
        }
        
    }
    
    // Controls the movement of the player.
    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if(moveX < 0f || moveX > 0f)
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
    
    // Kills the player if they collide with the pit.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.tag == "Pit")
        {
            isDead = true;
        }
    }

    // Plays the death animation and kills the player if they are hit by a projectile
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.tag == "Projectile")
        {
            isDead = true;
            anim.SetTrigger("Hit");
        }
    }
}
