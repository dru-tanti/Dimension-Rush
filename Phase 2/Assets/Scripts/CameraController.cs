using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 velocity;
    public Camera camera;

    [Header ("Camera Smoothing")]
    public float smoothTimeY;
    public float smoothTimeX;
    public float offset;
    [Tooltip ("Check if you want to define minimum and maximum camera position")]
    public bool bounds;

    [Header ("Camera Bounds")]
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        // Smoothens the camera movement
        float posX = Mathf.SmoothDamp(transform.position.x, Player.transform.position.x + offset, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, Player.transform.position.y + offset, ref velocity.y, smoothTimeY);    

        transform.position = new Vector3(posX, posY, transform.position.z);

        if(bounds)
        {
            // Defines the minimum and maximum position the camera can move
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.position = new Vector3(0f, 0f, transform.position.z);
            Camera.main.orthographicSize = 22f;
        } else {
            Camera.main.orthographicSize = 13f;
        }
    }
}
