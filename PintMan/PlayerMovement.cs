using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D myBody;    
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    private Animator anim;
    private bool isGrounded;
    private bool jumped;
    
    public float jumpPower = 5f;

    public AudioClip PintsClip,CripsClip,NutsClip,BastardsClip;
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
        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer))
        {
            //print("Collided with groud raycast");
        }
    }



    void FixedUpdate()
    {
        PlayerWalk();
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);       

        Console.WriteLine("ground");

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;

                //anim.SetBool("Jump", false);
            }
        }       
    }


    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            anim.Play("LostyRun");
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            
            ChangeDirection(1f);

        }
        else if (h < 0)
        {
            anim.Play("LostyRun");
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);

            ChangeDirection(-1f);
        }
        else
        {
            anim.Play("LostyIdle");
            myBody.velocity = new Vector2(0F, myBody.velocity.y);
        }

        //anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));


    }


    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                //anim.SetBool("Jump", true);
                Console.WriteLine("Player jump");

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Console.WriteLine("jumping!!!");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pint")
        {
           
            playerAudioData.PlayOneShot(PintsClip, 0.5f);
        }
        else if(collision.gameObject.tag == "Crisps")
        {
            
            playerAudioData.PlayOneShot(CripsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Nuts")
        {
          
            playerAudioData.PlayOneShot(NutsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Spike")
        {
            
            playerAudioData.PlayOneShot(BastardsClip, 0.5f);
        }
    }





















}//class
