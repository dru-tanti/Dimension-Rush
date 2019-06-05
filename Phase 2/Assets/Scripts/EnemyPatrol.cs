using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private bool isAttacking = false;
    [Header ("Enemy Movement Variables")]
    private bool isplaying;
    public float speed;
    private bool movingRight = true;
    public Transform patrolStart;
    public Transform patrolEnd;
    private Vector2 target;
    private Animator anim;

    [Header ("Enemy Attack Variables")]
    private bool angry;
    private float timeBtwShots;
    public float startTimeBtwShots;    
    public Transform pickaxeSpawn;
    public GameObject pickaxe;
    public float distance;

    private void Awake() 
    {
        anim = GetComponent<Animator>();   
    }

    private void Start() 
    {
        target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        if(!isAttacking)
        {
            Move();
        }
    }


    // Plays the roar when the attack is triggered for the first time.
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !angry)
        {
            AudioManager.current.Play("AntRoar");
            angry = true;        
        }
    }
    
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Player")
        { 
            anim.SetBool("isAttacking", true);
            isAttacking = true;
            if(timeBtwShots <= 0){
                timeBtwShots = startTimeBtwShots;
                Instantiate(pickaxe, pickaxeSpawn.position, Quaternion.identity);
            } else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        { 
            anim.SetBool("isAttacking", false);
            isAttacking = false;
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
