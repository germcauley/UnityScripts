using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{
    
    public NewHealthBarScript healthBar;
    public int currentHealth, maxHealth = 10;
    public float speed = 5f, jumpPower = 5f, moveForce = 5f, groundCheckDistance = 0.1f,knockBackPower;
    public Vector2 movement;
    private Rigidbody2D myBody;    
    public Transform groundCheckPosition;
    public LayerMask groundLayer;    
    private Animator anim;
    public GameObject Hitfx, deadHead,deadArm,deadLeg,deadBody,blood;
    private GameObject instantiatedObj;
    private bool jumped,playingJumpAudio = false, isGrounded,knockback = false,canDamage=true,rhEnemyHit=true,Dead=false;
    public AudioClip PintsClip,CripsClip,NutsClip,BastardsClip,JumpClip,CoughClip,DeathClip,SplashClip;
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
        Dead = false;
        canDamage = true;
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
        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer))
        {
            //print("Collided with groud raycast");
        }

        if (currentHealth <=0 && Dead ==false)
        {
            print("players has died!");
            Dead = true;
            StartCoroutine(PlayerDead());
            //new WaitForSeconds(2f);
            
            
        }
    }



    void FixedUpdate()
    {
        PlayerWalk();
        PlayerJump();
        if (knockback == true)
        {
            if (!rhEnemyHit)
            {
                Vector2 NewPosition = new Vector2(knockBackPower* -1, 10.0f);
                moveCharacter(NewPosition);
            }
            else
            {
                Vector2 NewPosition = new Vector2(knockBackPower, 10.0f);
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
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, groundLayer);        
        if (isGrounded)
        {            
           jumped = false;           
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
            if (Input.GetKey(KeyCode.Space) && isGrounded && jumped ==false)
            {
                print("jumped!!!!");
                jumped = true;                
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            if (!playingJumpAudio)
            {
                playingJumpAudio = true;
                StartCoroutine(PlayJumpAudioClip());
            }
                
                //anim.SetBool("Jump", true);               

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
                StartCoroutine(PlayerDead());
                //StartCoroutine(DamagePlayer(10));

            }
            else if (collision.gameObject.tag == "Virus")
            {
                Vector2 NewPosition = new Vector2(-100f, 10.0f);
                moveCharacter(NewPosition);

                StartCoroutine(InfectPlayer());
            }
            else if (collision.gameObject.name == "GardaWalk")
            {
                print("TAKEMONEY");
                //take 1 pint from score and dage -1 ,if pints are 0 damage -2
            }
            else if (collision.gameObject.tag == MyTags.MOVING_PLATFORM_TAG)
            {
                var emptyObject = new GameObject();
                emptyObject.transform.parent = collision.gameObject.transform;
                gameObject.transform.parent = emptyObject.transform;

                //gameObject.transform.SetParent(collision.gameObject.transform);                
               // Debug.Log("On platform.");              

            }
            else if (collision.gameObject.tag == MyTags.GARDA_CAR_TAG)
            {
                knockBackPower = 400f;
                CameraShake();
                StartCoroutine(PlayerDead());
                Debug.Log("Hit by car!!!");

            }           
            else if (collision.gameObject.tag == MyTags.BOSS_TAG)
            {
                //knockBackPower = 400f;
                //CameraShake();
                //currentHealth -= 10;
                //StartCoroutine(DamagePlayer(10));
                Debug.Log("Hit by BOss!!!");

            }
            else if (collision.gameObject.name == "ENDLEVEL")
            {
                StartCoroutine(Restart());
            }
        }
        else
        {
            //print("Invincilbe, cannot damage");
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
         if (collision.gameObject.tag == MyTags.MOVING_PLATFORM_TAG)
        {

            gameObject.transform.parent = null;
            Debug.Log("Not on platform.");

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
        else if (collision.gameObject.tag == "Bullet" && canDamage)
        {                
            StartCoroutine(BulletHitAnim(collision));            
            CameraShake();
            StartCoroutine(DamagePlayer(1));
        }
        else if (collision.gameObject.tag == MyTags.BAG_OF_SPUDS)
        {
            //knockBackPower = 400f;
            CameraShake();
            StartCoroutine(DamagePlayer(3));

        }
        else if (collision.gameObject.tag == "Water")
        {
            playerAudioData.PlayOneShot(SplashClip, 0.5f);
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
        yield return new WaitForSecondsRealtime(2f);
        RestartScene();
    }

    IEnumerator Knockback()
    {
        knockback = true;
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
        knockBackPower = 100f;
    }

    IEnumerator PlayJumpAudioClip()
    {
        playerAudioData.PlayOneShot(JumpClip, 0.2f);
        yield return new WaitForSeconds(0.3f);
        playingJumpAudio = false;
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

    IEnumerator VirusDamageColour()
    {
        rend.material.color = Color.green;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.white;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.green;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.white;
    }

    IEnumerator IncreaseHP()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        currentHealth += 1;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator DamagePlayer(int damage)
    {
        canDamage = false;        
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        yield return new WaitForSecondsRealtime(0.1f);
        playerAudioData.PlayOneShot(BastardsClip, 0.5f);        
        StartCoroutine(DamageColour());        
        StartCoroutine(Knockback());
        StartCoroutine(Invincible());
    }

    IEnumerator InfectPlayer()
    {
        int sickness = 0;
        canDamage = false;        
        yield return new WaitForSecondsRealtime(0.1f);        
        StartCoroutine(Knockback());
        while (sickness < 5)
        {
            playerAudioData.PlayOneShot(CoughClip, 0.5f);
            StartCoroutine(VirusDamageColour());
            yield return new WaitForSecondsRealtime(2.0f);
            currentHealth -= 1;
            healthBar.SetHealth(currentHealth);
            sickness += 1;
        }
        canDamage = true;
    }

    IEnumerator Invincible()
    {        
        yield return new WaitForSecondsRealtime(1f);
        canDamage = true;       
    }

    //For when enemy bullets hit the player
    IEnumerator BulletHitAnim(Collider2D collision)
    {
        Destroy(collision.gameObject);
        instantiatedObj = (GameObject)Instantiate(Hitfx, new Vector3(collision.transform.position.x,
                    collision.transform.position.y, collision.transform.position.z), Quaternion.identity);

        yield return new WaitForSecondsRealtime(0.7f);

        Destroy(instantiatedObj);

    }

    IEnumerator PlayerDead()
    {
        if (!Dead)
        {
            Dead = true;
            canDamage = false;
            healthBar.SetHealth(0);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            playerAudioData.PlayOneShot(DeathClip, 0.5f);
            Instantiate(blood, transform.position, Quaternion.identity);
            Instantiate(deadHead, transform.position, Quaternion.identity);
            Instantiate(deadArm, transform.position, Quaternion.identity);
            Instantiate(deadBody, transform.position, Quaternion.identity);
            Instantiate(deadLeg, transform.position, Quaternion.identity);            
        }        
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(Restart());
    }











}//class
