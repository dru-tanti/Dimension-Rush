using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Enemy Movement Variables")]
    public float speed;
    private bool movingRight = true;
    public Transform patrolStart;
    public Transform patrolEnd;
    private Vector2 target;

    [Header ("Enemy Attack Variables")]
    private float timeBtwShots;
    public float startTimeBtwShots;    
    public Transform pickaxeSpawn;
    public GameObject pickaxe;
    public float distance;



    private void Start() 
    {
        target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        Move();
        PlayerSearch();
    }

    private IEnumerator ThrowPickaxe()
    {
        yield return new WaitForSeconds(2f);
    }

    void PlayerSearch()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
            if(hitInfo.collider.CompareTag("Player"))
            { 

                if(timeBtwShots <= 0){
                    Instantiate(pickaxe, pickaxeSpawn.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                } else
                {
                    timeBtwShots -= Time.deltaTime;
                }
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
                transform.localScale = new Vector3(-1f, 1f, 1f);
                movingRight = false;
            } else {
                target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
                movingRight = true;
            }
        }
    }
}
