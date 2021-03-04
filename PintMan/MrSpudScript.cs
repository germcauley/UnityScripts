using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrSpudScript : MonoBehaviour
{
    public GameObject bagOfSpuds, Pintman;
    public Transform attackInstantiate;
    private Animator anim;
    private bool stunned = false;
    public LayerMask playerLayer;
    private string coroutine_Name = "StartAttack";
    public float ProjectileSpeed, fireRate, nextFire;
    public AudioClip BossPainClip;
    private AudioSource BossAudioData;
    public Transform EnemyHeadCollider;


    void Start()
    {
        Pintman = GameObject.Find("PintMan");
        BossAudioData = gameObject.GetComponent<AudioSource>();
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
        //if (transform.position.x > Pintman.transform.position.x)
        //{
        //    transform.localScale = new Vector2(-0.15f, 0.15f);
        //}
        //else if (transform.position.x < Pintman.transform.position.x)
        //{
        //    transform.localScale = new Vector2(0.15f, 0.15f);
        //}

        //if (GardaHealth <= 0)
        //{
        //    StartCoroutine(DestroyEnemy());
        //}
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


        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.2f, playerLayer);
        //if player jumps on enemy head it can stun or kill
        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                print("YOU JUMPED ON MR SPUDS HEAD!!!!!");

                if (!stunned)
                {
                    BossAudioData.PlayOneShot(BossPainClip, 0.8f);
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    //StartCoroutine(DestroyEnemy());
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



}
