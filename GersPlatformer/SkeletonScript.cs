using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x : MonoBehaviour
{
    public bool moveRight;
    public float moveSpeed = 1f;

    //public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    public Transform top_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;

    public LayerMask playerLayer;
    private bool stunned;
    private Animator anim;

    public AudioClip ouchclip;
    AudioSource skeletonAudioData;
    // Start is called before the first frame update

    void Awake()
    {
        //myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        skeletonAudioData = GetComponent<AudioSource>();
    }

    // Use this for initialization

    void Update()
    {

        // Use this for initialization
        if (!stunned)
        {
            if (moveRight)
            {
                transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
                transform.localScale = new Vector2(6, 6);
            }

            else
            {
                transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
                transform.localScale = new Vector2(-6, 6);
            }
        }
        else
        {
            transform.Translate(0 * Time.deltaTime * moveSpeed, 0, 0);
        }
       

        CheckCollision();
    }

    void CheckCollision()
    {

        //RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        //RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

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

                    //canMove = false;
                    //myBody.velocity = new Vector2(0, 0);
                    
                    print("Skel stun");
                    skeletonAudioData.PlayOneShot(ouchclip, 0.7F);
                    anim.Play("StunSkelly");
                    stunned = true;
                    StartCoroutine(ResetWalk());

                    //// BEETLE CODE HERE
                    //if (tag == MyTags.BEETLE_TAG)
                    //{
                    //    anim.Play("StunSkel");
                    //    StartCoroutine(Dead(0.5f));
                    //}
                }
            }
        }

        //if (leftHit)
        //{
        //    print("LEFT HIT");
        //    if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
        //    {
        //        if (!stunned)
        //        {
        //            // APPLY DAMAGE TO PLAYER
        //            leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
        //        }
        //        else
        //        {
        //            if (tag != MyTags.BEETLE_TAG)
        //            {
        //                myBody.velocity = new Vector2(15f, myBody.velocity.y);
        //                StartCoroutine(Dead(3f));
        //            }
        //        }
        //    }
        //}

        //if (rightHit)
        //{
        //    print("RIGHT HIT");
        //    if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
        //    {
        //        if (!stunned)
        //        {
        //            // APPLY DAMAGE TO PLAYER
        //            rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
        //        }
        //        else
        //        {
        //            if (tag != MyTags.BEETLE_TAG)
        //            {
        //                myBody.velocity = new Vector2(-15f, myBody.velocity.y);
        //                StartCoroutine(Dead(3f));
        //            }
        //        }
        //    }
        //}

        //// IF we don't detect collision any more do whats in {}
        //if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        //{

        //    ChangeDirection();
        //}

    }
    IEnumerator ResetWalk()
    {
        yield return new WaitForSeconds(2f);
        anim.Play("skellytest");
        stunned = false;
        print("Walk");
    }

    void OnTriggerEnter2D(Collider2D trig)

    {
        if (trig.gameObject.CompareTag("turn"))
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
    }
}
