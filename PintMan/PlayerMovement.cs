using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{
    
    public NewHealthBarScript healthBar;
    public int currentHealth, maxHealth = 10;
    public float speed = 5f, jumpPower = 5f, moveForce = 5f;
    public Vector2 movement;
    private Rigidbody2D myBody;    
    public Transform groundCheckPosition;
    public LayerMask groundLayer;    
    private Animator anim;
    public GameObject Hitfx;    
    private bool jumped, isGrounded,knockback = false,canDamage=true,rhEnemyHit=true;
    public AudioClip PintsClip,CripsClip,NutsClip,BastardsClip,JumpClip;
    AudioSource playerAudioData;
    //Reference to camera and overlay sprite unused
    private GameObject cam, Overlay;
    // Reference to Sprite Renderer component
    private Renderer rend, overlayRend;

    // Color value that we can set in Inspector
    // It's White by default
    [SerializeField]
    private Color colorToTurnTo = Color.white;

    //[SerializeField]
    //private Color OverlaycolorToTurnTo = Color.white;

    void Awake()
    {
        
        cam = GameObject.Find("CameraShaker");
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
        Overlay = GameObject.Find("Overlay");
        overlayRend = Overlay.GetComponent<Renderer>();



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

        if (knockback == true)
        {
            if (!rhEnemyHit)
            {
                Vector2 NewPosition = new Vector2(-100f, 10.0f);
                moveCharacter(NewPosition);
            }
            else
            {
                Vector2 NewPosition = new Vector2(100f, 10.0f);
                moveCharacter(NewPosition);
            }         
        }
    }


    void CameraShake()
    {
        
        cam.GetComponent<CameraShakeScript>().ShakeIt();       
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
                playerAudioData.PlayOneShot(JumpClip, 0.1f);
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
        if (canDamage)
        {
            //set direction of enemy attack, this will determine playerknockback direction
             if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {

                rhEnemyHit = false;
            }
            else if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {

                rhEnemyHit = true;
            }

            
            if (collision.gameObject.tag == "Spike")
            {
                CameraShake();
                StartCoroutine(DamagePlayer());

            }
            else if (collision.gameObject.tag == "Virus")
            {
                Vector2 NewPosition = new Vector2(-100f, 10.0f);
                moveCharacter(NewPosition);

                StartCoroutine(DamagePlayer());
            }
            else if (collision.gameObject.name == "GardaWalk")
            {
                print("TAKEMONEY");
                //take 1 pint from score and dage -1 ,if pints are 0 damage -2
            }
            else if (collision.gameObject.name == "ENDLEVEL")
            {
                StartCoroutine(Restart());
            }
        }
        else
        {
            print("Invincilbe, cannot damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.PINT_TAG)
        {
            StartCoroutine(IncreaseHP());
            playerAudioData.PlayOneShot(PintsClip, 0.5f);
            //overlayRend.material.color = OverlaycolorToTurnTo;
            
        }
        else if (collision.gameObject.tag == MyTags.CRISPS_TAG)
        {
            StartCoroutine(IncreaseHP());
            playerAudioData.PlayOneShot(CripsClip, 0.5f);
        }
        else if (collision.gameObject.tag == MyTags.NUTS_TAG)
        {
            StartCoroutine(IncreaseHP());
            playerAudioData.PlayOneShot(NutsClip, 0.5f);
        }
        else if (collision.gameObject.tag == "Bullet")
        {

            Instantiate(Hitfx, new Vector3(collision.transform.position.x,
                    collision.transform.position.y - 1f, collision.transform.position.z), Quaternion.identity);
            Destroy(collision.gameObject);
            CameraShake();
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
        knockback = true;
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;       
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
        StartCoroutine(Invincible());
        currentHealth -= 1;
        healthBar.SetHealth(currentHealth);
        yield return new WaitForSecondsRealtime(0.1f);
        playerAudioData.PlayOneShot(BastardsClip, 0.5f);        
        StartCoroutine(DamageColour());        
        StartCoroutine(Knockback());
    }

    IEnumerator Invincible()
    {
        canDamage = false;
        yield return new WaitForSecondsRealtime(0.7f);
        canDamage = true;
    }











}//class
