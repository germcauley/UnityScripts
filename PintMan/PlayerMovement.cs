using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{

    public float speed = 5f;
    public Vector2 movement;
    public Rigidbody2D myBody;    
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    public static float healthAmount;
    private Animator anim;
    private bool isGrounded;
    private bool jumped,spikes=false;
    public float jumpPower = 5f, moveForce = 5f;
    public AudioClip PintsClip,CripsClip,NutsClip,BastardsClip;
    AudioSource playerAudioData;
    void Awake()
    {
        Time.timeScale = 1.0f;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudioData = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = 1;
        Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        CheckIfGrounded();
        PlayerJump();
        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer))
        {
            //print("Collided with groud raycast");
        }

        if (healthAmount<0)
        {
            Destroy(gameObject);
        }
    }



    void FixedUpdate()
    {
        PlayerWalk();
        if (spikes ==true)
        {
            Vector2 NewPosition = new Vector2(-100f, 20.0f);
            moveCharacter(NewPosition);
        }
        

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

    void moveCharacter(Vector2 direction)
    {
        myBody.AddForce(direction * speed);
    }

    //Called when PLayers collides with various objects   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Spike")
        {
            playerAudioData.PlayOneShot(BastardsClip, 0.5f);
            healthAmount -= 0.1f;
            //StartCoroutine(Restart());
            StartCoroutine(Knockback());

        }
        else if (collision.gameObject.tag == "Bullet")
        {
            //playerAudioData.PlayOneShot(BastardsClip, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pint")
        {

            playerAudioData.PlayOneShot(PintsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Crisps")
        {

            playerAudioData.PlayOneShot(CripsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Nuts")
        {

            playerAudioData.PlayOneShot(NutsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Water")
        {
            
            StartCoroutine(Restart());
        }
    }



    public void RestartScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }


    //CoRoutines

    IEnumerator Restart()
    {
        Time.timeScale = 0.0f;
        playerAudioData.PlayOneShot(BastardsClip, 0.5f);
        yield return new WaitForSecondsRealtime(2f);
        RestartScene();
    }

    IEnumerator Knockback()
    {
        spikes = true;
        yield return new WaitForSecondsRealtime(0.1f);
        spikes = false;
    }
















}//class
