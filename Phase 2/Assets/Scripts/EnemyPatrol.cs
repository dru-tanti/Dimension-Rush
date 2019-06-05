using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform patrolStart;
    public Transform patrolEnd;
    private Vector2 target;
    public Transform pickaxeSpawn;
    public GameObject pickaxe;
    public float distance;



    private void Start() 
    {
        target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
    }

    void Update()
    {
        Move();
        PlayerSearch();
    }

    void ThrowPickaxe()
    {
        Instantiate(pickaxe, pickaxeSpawn.position, pickaxeSpawn.rotation);
    }

    void PlayerSearch()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, distance);
        if(hitInfo.collider != null) {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        } else {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            if(movingRight)
            {
                target = new Vector2(patrolStart.position.x, patrolStart.position.y);
                movingRight = false;
            } else {
                target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
                movingRight = true;
            }
        }
    }
}
