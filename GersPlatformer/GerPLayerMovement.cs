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
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    public float jumpPower = 5f;
    AudioSource playerAudioData;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }




    // Start is called before the first frame update
    void Start()
    {
        playerAudioData = GetComponent<AudioSource>();
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
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
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
            }
            
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

                anim.SetBool("Jump", true);
                Console.WriteLine("Player jump");
                playerAudioData.Play(0);
            }
        }
    }


}//class


















