using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

    private bool grounded = false;
    private Rigidbody2D _playerRB;

    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if(h * _playerRB.velocity.x < maxSpeed)
        {
            _playerRB.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs (_playerRB.velocity.x) > maxSpeed)
        {
            _playerRB.velocity = new Vector2(Mathf.Sign (_playerRB.velocity.x) * maxSpeed, _playerRB.velocity.y);
        }

        if (jump)
        {
            _playerRB.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
