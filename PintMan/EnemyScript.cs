using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public Transform EnemyHeadCollider;
    public LayerMask playerLayer;
    public float BulletSpeed;
    private GameObject HitFX; 
    private bool stunned = false;
    private Animator anim;
    [SerializeField]
    GameObject bullet;
    public GameObject Pintman;    
    public float fireRate;
    float nextFire;
    public AudioClip DeathClip, GunClip;
    AudioSource enemyAudioData;


    
    void Start()
    {
       // anim.Play("GardaIdle");
        fireRate = 2f;
        nextFire = Time.time;
        anim = gameObject.GetComponent<Animator>();
        HitFX = gameObject.transform.GetChild(2).gameObject;
        HitFX.SetActive(false);
        Pintman = GameObject.Find("PintMan");
        enemyAudioData = GetComponent<AudioSource>();
    }

    
    void Update()
    {

        //CheckIfHit();
        CheckCollision();
        if (Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.5f, playerLayer))
        {
            //print("Collided with enemy");
        }

        //Makes the Enemy sprite face the direction of the player character byt flipping x axis when required
        if (transform.position.x > Pintman.transform.position.x)
        {
            
            transform.localScale = new Vector2(-0.15f, 0.15f);
        }
        else if (transform.position.x < Pintman.transform.position.x)
        {
           
            transform.localScale = new Vector2(0.15f, 0.15f);
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
        bullet.GetComponent<Bullet>().moveSpeed = BulletSpeed;
        enemyAudioData.PlayOneShot(GunClip, 0.5f);
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
                StartCoroutine(DestroyEnemy());   
                }
            }          
        }
    }


    IEnumerator DestroyEnemy()
    {

        HitFX.SetActive(true);
        enemyAudioData.PlayOneShot(DeathClip, 0.1f);
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.SetActive(false);
    }


    }//class
