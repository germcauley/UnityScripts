using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrSpudScript : MonoBehaviour
{
    public GameObject bagOfSpuds, Pintman;
    public Transform attackInstantiate;
    private Animator anim;
    private bool stunned = false,hit=false;
    public LayerMask playerLayer;
    private string coroutine_Name = "StartAttack";
    public float ProjectileSpeed, fireRate, nextFire;
    public AudioClip BossPainClip;
    private AudioSource BossAudioData;
    public Transform EnemyHeadCollider;
    private Rigidbody2D pintManRB;
    public Vector2 movement;


    void Start()
    {
        Pintman = GameObject.Find("PintMan");
        BossAudioData = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        pintManRB = Pintman.GetComponent<Rigidbody2D>();
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
            transform.localScale = new Vector2(2f, 2f);
        }
        else if (transform.position.x < Pintman.transform.position.x)
        {
            transform.localScale = new Vector2(-2f, 2f);
        }

       
        movement = new Vector2(100f, 0f);
    }

    private void FixedUpdate()
    {
        if (hit)
        {
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        pintManRB.AddForce(direction * 10f);
    }


    void Attack()
    {

       // GameObject obj = Instantiate(bagOfSpuds, attackInstantiate.position, Quaternion.identity);
       /// obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700), 0f));

        Instantiate(bagOfSpuds, transform.position, Quaternion.identity);
        bagOfSpuds.GetComponent<BagOfSpudsScript>().moveSpeed = ProjectileSpeed;
    }

    void BackToIdle()
    {
        anim.Play("BossAnimation");
    }

    //check if player has jumped on head
    void CheckCollision()
    {


        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.5f, playerLayer);
        //if player jumps on enemy head it can stun or kill
        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {             
                if (!stunned)
                {                
                    stunned = true;
                    hit = true;
                    print("BOSS is INVINCIBLE");
                    StartCoroutine(Stunned());                   
                }
            }
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("BossAttack");
        StartCoroutine(coroutine_Name);
    }


    IEnumerator Stunned()
    {
        BossAudioData.PlayOneShot(BossPainClip, 0.8f);
        anim.SetBool("Stunned", true);        
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Stunned", false);
        stunned = false;
        print("BOSS NO LONGER INVINCIBLE");
    }
   
}
