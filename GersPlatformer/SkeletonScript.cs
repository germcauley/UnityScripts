using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSkeleton : MonoBehaviour
{
    private bool moveRight;
    public bool canWalk = true;
    public float moveSpeed = 1f;
    private bool canDamage =true;
    public bool attacking = false;
    public bool weapon;
    private bool alive = true;

    //public Transform left_Collision, right_Collision, top_Collision;//, down_Collision;
    public Transform top_Collision;
    public Transform Player;
    private Vector3 left_Collision_Pos, right_Collision_Pos;
    private Rigidbody2D myBody;
    public LayerMask playerLayer;
    private bool stunned;
    private Animator anim;

    public int health = 1;
    public AudioClip ouchclip;
    public AudioClip dieClip;
    public AudioClip attackClip;
    AudioSource skeletonAudioData;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        skeletonAudioData = GetComponent<AudioSource>();
        //make player weapon inactive
        gameObject.transform.GetChild(1).gameObject.SetActive(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        // Use this for initialization
        if (moveRight && canWalk)
        {
            transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(6, 6);
            //print("walk");
        }

        else if (!moveRight && canWalk)
        {
            transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(-6, 6);
            //print("walk");
        }

        else
        {
            transform.Translate(0 * Time.deltaTime * moveSpeed, 0, 0);
        }

        CheckCollision();
        
       
    }


    void CheckCollision()
    {

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                print("Tophit detected");
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    StunSkel();                    
                }
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D target)
    {

        if (target.gameObject.CompareTag("turn"))
        {
            if (moveRight)
            {
                moveRight = false;
                
            }
            else
            {
                moveRight = true;
                
            }
        }
        if (target.gameObject.CompareTag("Sword") && target.gameObject.activeInHierarchy == true)
        {
            //print("sword status: "+target.gameObject.activeInHierarchy);
            //print("Skelly hit by sword");            
            DealDamage();
            
        }
    }

    public void Attack()
    {

        // if attack boole is true loop attack
        if (attacking == true && alive == true)
        {
            Debug.Log(transform.position.x);
            Debug.Log(Player.position.x);
            //if player X pos is less than Enemy flip to face player
            if (transform.position.x > Player.position.x)
            {
                transform.localScale = new Vector2(-6, 6);
            }
            else if (transform.position.x < Player.position.x)
            {
                transform.localScale = new Vector2(6, 6);
            }
            canWalk = false;
            //STOP THE SKELETON MOVING TOTALLY
            print("SKELLY STOPPED");           
            anim.SetBool("Attack", true);
            //make player weapon active
            gameObject.transform.GetChild(1).gameObject.SetActive(weapon);

        }
        else if (attacking == false && alive == true)
        {
            // if attack bool is false stop it
            anim.SetBool("Attack", false);
            StartCoroutine(ResetWalk());
        }
                  
    }

    void AttackAndSounds()
    {
            skeletonAudioData.PlayOneShot(attackClip, 0.7F);
            //StartCoroutine(ResetWalk());    
        
    }

    public void DealDamage()
    {

        if (canDamage && health > 1)
        {
            StunSkel();
            health--;
            //print("Skel hit!");
           // print("HEALTH IS "+health);
            canDamage = false;
            StartCoroutine(ResetWalk());
            StartCoroutine(WaitForDamage());
        }
        else if (canDamage && health == 1)
        {
            health--;
            canWalk = false;
            alive = false;
            //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.02f, 0.12f);
            //gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.12f,0.10f);
            anim.Play("SkellyDead");            
            StartCoroutine(KillSkelly());
            
        }

    }

    public void StunSkel()
    {
        skeletonAudioData.PlayOneShot(ouchclip, 0.7F);
        anim.Play("SkellyStun");
        stunned = true;
        StartCoroutine(ResetWalk());
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }


    IEnumerator ResetWalk()
    {
        yield return new WaitForSeconds(0.75f);
        canWalk = true;
        anim.Play("SkellyWalk");
        stunned = false;       
    }

    IEnumerator KillSkelly() 
    {
        print("Kill skelly");
        yield return new WaitForSeconds(0.5f);
        skeletonAudioData.PlayOneShot(dieClip, 0.7F);
        yield return new WaitForSeconds(5.5f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }


    IEnumerator ResetIdle()
    {
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("Attack", false);
       
    }

}//class












