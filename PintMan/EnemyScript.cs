using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public Transform EnemyHeadCollider;
    public LayerMask playerLayer;
    private bool stunned = false;
    private Animator anim;
    [SerializeField]
    GameObject bullet;

    public float fireRate;
    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
       // anim.Play("GardaIdle");
        fireRate = 2f;
        nextFire = Time.time;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //CheckIfHit();
        CheckCollision();
        if (Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.5f, playerLayer))
        {
            //print("Collided with enemy");
        }

        //CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        //if (Time.time > nextFire)
        //{
        //    Instantiate(bullet, transform.position, Quaternion.identity);
        //    nextFire = Time.time + fireRate;
        //}       
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;        
    }

    // check for player collision using raycast
    void CheckCollision()
    {
        

        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.2f, playerLayer);
        
        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                print("Enemy hit!!!");
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                }
            }          
        }
    }





    }//class
