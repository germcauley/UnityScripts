using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GerPlayerMovement : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;


    public Transform groundCheckPosition;
    public LayerMask groundLayer,enemyLayer;
    //public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    public Transform right_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;

    private bool isGrounded;
    private bool jumped;
    

    public float jumpPower = 5f;
    public AudioClip jumpClip;
    public AudioClip landClip;
    public AudioClip walkclip;
    public AudioClip swordclip;


    AudioSource playerAudioData;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudioData = GetComponent<AudioSource>();

    }




    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("started");
    }

    // Update is called once per frame

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
        PlayerWalk();
        PlayerAttack();
        PlayerSounds();
        
        
        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer))
        {
            //print("Collided with groud raycast");
        }


     }
   
    void FixedUpdate()
    {
        //PlayerWalk();
    }

    void PlayerWalk()
    {       

        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            anim.SetBool("isRunning", true);
            ChangeDirection(5);
           

        }
        else if (h < 0)
        {
            
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            anim.SetBool("isRunning", true);     
            ChangeDirection(-5);
            

        }
        else
        {
            myBody.velocity = new Vector2(0F, myBody.velocity.y);
            anim.SetBool("isRunning", false);          
            CancelInvoke();
            
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

   

    void PlayerSounds()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {             
                playerAudioData.PlayOneShot(walkclip, 0.7F);
            }   
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAudioData.Stop();
        }
        
    }


    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        //print(isGrounded + "grounded!");

        if (isGrounded)
            
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);  
                playerAudioData.PlayOneShot(landClip, 0.5F);
                print("Player land");
            }
        }
       
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                print("Player jump");
                
                playerAudioData.PlayOneShot(jumpClip, 0.5F);
                anim.SetBool("Jump", true);

                StartCoroutine(setJumped());
            }
        }
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetBool("Attack", true);
            playerAudioData.PlayOneShot(swordclip, 0.5f);
            print("PlayerAttack");
            CheckCollision();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("Attack", false);
            print("PlayerStopAttack");
        }
    }

    IEnumerator setJumped()
    {
        yield return new WaitForSeconds(0.5f);
        print("SET JUMP CO ROUTINE");
        jumped = true;
    }


    void CheckCollision()
    {
        print("check collision");
        //RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, enemyLayer);
        RaycastHit2D SwordHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, enemyLayer);

        //Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, enemyLayer);

        
        //if (leftHit)
        //{
        //    if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
        //    {
     
        //        // APPLY DAMAGE TO PLAYER
        //        leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();

        //    }
        //}

        if (SwordHit)
        {
            print("hit skeleton!!!");
            if (SwordHit.collider.gameObject.tag == MyTags.SKELETON_TAG)
            {
                print("hit skeleton!!!");
                SwordHit.collider.gameObject.GetComponent<x>().StunSkel();
                // rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();

            }
        }

        //// IF we don't detect collision any more do whats in {}
        //if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        //{

        //    ChangeDirection();
        //}

    }


}//class


















