using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{
    public int maxHealth = 10;
    public NewHealthBarScript healthBar;
    public int currentHealth;
    public float speed = 5f;
    public Vector2 movement;
    private Rigidbody2D myBody;    
    public Transform groundCheckPosition;
    public LayerMask groundLayer;    
    private Animator anim;
    private bool isGrounded;
    private bool jumped,spikes=false;
    public float jumpPower = 5f, moveForce = 5f;
    public AudioClip PintsClip,CripsClip,NutsClip,BastardsClip;
    AudioSource playerAudioData;
    
    // Reference to Sprite Renderer component
    private Renderer rend;

    // Color value that we can set in Inspector
    // It's White by default
    [SerializeField]
    private Color colorToTurnTo = Color.white;

    void Awake()
    {
        Time.timeScale = 1.0f;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudioData = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        // Assign Renderer component to rend variable
        rend = GetComponent<Renderer>();

        // Change sprite color to selected color
        

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

        if (currentHealth <=0)
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
        //Using collider here as player cant fal thorugh spikes
        if (collision.gameObject.tag == "Spike")
        {

            StartCoroutine(DamagePlayer());

        }      
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pint")
        {
            StartCoroutine(IncreaseHP());
            playerAudioData.PlayOneShot(PintsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Crisps")
        {
            StartCoroutine(IncreaseHP());
            playerAudioData.PlayOneShot(CripsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Nuts")
        {
            StartCoroutine(IncreaseHP());
            playerAudioData.PlayOneShot(NutsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            StartCoroutine(DamagePlayer());
        }
        else if (collision.gameObject.tag == "Virus")
        {
            StartCoroutine(DamagePlayer());
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

    IEnumerator DamageColour()
    {
        rend.material.color = colorToTurnTo;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.white;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = colorToTurnTo;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.white;
    }

    IEnumerator IncreaseHP()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        currentHealth += 1;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator DamagePlayer()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        playerAudioData.PlayOneShot(BastardsClip, 0.5f);
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(DamageColour());
        //StartCoroutine(Restart());
        StartCoroutine(Knockback());
    }











}//class
