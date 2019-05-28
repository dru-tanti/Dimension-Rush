using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public float offset;

    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x + offset, ref velocity.x, smoothTimeX);
        // float posY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y + offset, ref velocity.y, smoothTimeY);    

        transform.position = new Vector3(posX, 0, transform.position.z);
    }
}

