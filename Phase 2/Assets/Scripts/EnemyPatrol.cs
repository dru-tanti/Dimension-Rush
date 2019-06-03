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

    private void Start() 
    {
        target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
    }

    void Update()
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
