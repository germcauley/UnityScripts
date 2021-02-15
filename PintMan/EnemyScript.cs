using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public Transform EnemyHeadCollider;
    public LayerMask playerLayer;
    public float BulletSpeed, fireRate, nextFire;
    private GameObject HitFX,BulletHitfx, instantiatedObj,DetectorChildObj;
    public int GardaHealth = 5;
    private bool stunned = false;
    private Animator anim;
    [SerializeField]
    GameObject bullet;
    public GameObject Pintman;      
    public AudioClip DeathClip, GunClip;
    AudioSource enemyAudioData;


    
    void Start()
    {
       // anim.Play("GardaIdle");
        fireRate = 1f;
        nextFire = Time.time;
        anim = gameObject.GetComponent<Animator>();
        HitFX = gameObject.transform.GetChild(2).gameObject;
        DetectorChildObj = gameObject.transform.GetChild(1).gameObject;
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

        if (GardaHealth <= 0)
        {
            StartCoroutine(DestroyEnemy());
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().moveSpeed = BulletSpeed;
            enemyAudioData.PlayOneShot(GunClip, 0.5f);
            nextFire = Time.time + fireRate;
        }

    }

    // check for player collision using raycast
    void CheckCollision()
    {
        

        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.2f, playerLayer);
        //if player jumps on enemy head it can stun or kill
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Physics2D.IgnoreCollision(collision, DetectorChildObj.GetComponent<Collider2D>());
       
        if (collision.gameObject.tag == MyTags.SPUD_TAG)
        {                  
            GardaHealth -= 1;            
        }
    }



    IEnumerator BulletHitAnim(Collider2D collision)
    {
        Destroy(collision.gameObject);
        instantiatedObj = (GameObject)Instantiate(BulletHitfx, new Vector3(collision.transform.position.x,
                    collision.transform.position.y, collision.transform.position.z), Quaternion.identity);

        yield return new WaitForSecondsRealtime(0.7f);

        Destroy(instantiatedObj);

    }


    IEnumerator DestroyEnemy()
    {

        HitFX.SetActive(true);
        enemyAudioData.PlayOneShot(DeathClip, 0.1f);
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.SetActive(false);
    }


    }//class
