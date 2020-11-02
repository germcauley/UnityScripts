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
    public AudioClip jumpClip;
    public AudioClip landClip;
    public AudioClip walkclip;


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
            print("PlayerAttack");          
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

    
}//class


















