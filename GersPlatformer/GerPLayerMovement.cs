using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerPlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public float movespeed = 1f;  

    private Rigidbody2D myBody;
    private Transform playerTransform;
    private Animator anim;    
    public Transform groundCheckPosition;
    private Text lifeText,gameOverText;
    public int lifeScoreCount;
    public LayerMask groundLayer, enemyLayer;
    public Transform right_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;
    public ParticleSystem blood;

    private bool isGrounded;
    private bool jumped;
    private bool fall,walking = false;
    private bool canDamage = true;

    public float jumpPower = 5f;
    public AudioClip jumpClip;
    public AudioClip ouchClip;
    public AudioClip landClip;
    public AudioClip walkclip;
    public AudioClip swordclip;
    AudioSource playerAudioData;



    void Awake()
    {
        
        myBody = GetComponent<Rigidbody2D>();        
        anim = GetComponent<Animator>();
        playerAudioData = GetComponent<AudioSource>();

        lifeText = GameObject.Find("LivesText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        gameOverText.gameObject.SetActive(false);
        //lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;
        blood.Stop();
        //print(blood.isPlaying);

        //Vars for player distance over time when hit
       

}

    // Start is called before the first frame update
    void Start()
    {
        //make player weapon inactive
        gameObject.transform.GetChild(1).gameObject.SetActive(false);       

    }

    // Update is called once per frame

    void Update()
    {       

        CheckIfGrounded();
        PlayerJump();
        PlayerAttack();
        PlayerWalk();
        PlayerSounds();        






        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer))
        {
            //print("Collided with groud raycast");
        }

    }

    void FixedUpdate()
    {
        
        
    }


   

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.J))
        {
            //Debug.Log(transform.position);
            anim.Play("PlayerAttack", 0, 0f);
            //print("attack!!");
            playerAudioData.PlayOneShot(swordclip, 0.5f);
            
            CheckCollision();
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("Attack", false);
            
        }
        else if (h > 0)
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
            
            walking = false;
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
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                walking = true;
               
                playerAudioData.loop = true;
                playerAudioData.clip = walkclip;
                playerAudioData.Play();

            }
        }
        else if (!isGrounded)
        {
            //if (jumped ==false)
            //{
            //    playerAudioData.Stop();
            //}
           
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                walking = true;               
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {

            //print("Walk Audiostop");
            playerAudioData.Stop();
            walking = false;
        }


    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer);
        //print(isGrounded + "grounded!");
        //fall = false;
        if (isGrounded)

        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
               
                //print("Player land");
                if (walking)
                {
              
                    //print("Land Walk Audiostart");
                    playerAudioData.loop = true;
                    playerAudioData.clip = walkclip;
                    playerAudioData.Play();

                }
            }
            if (fall)
            {
                
                print("Player fall and land");
                //anim.SetBool("Fall", true);
                playerAudioData.PlayOneShot(landClip, 0.5F);
                fall = false;
            }
            
        }
        else if (!isGrounded)
        {
         
            if (!jumped)
            {
                fall = true;
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                playerAudioData.Stop();
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
            //make weapon active on attack animation
            gameObject.transform.GetChild(1).gameObject.SetActive(true);          
            //print(gameObject.transform.GetChild(0).gameObject.activeInHierarchy);
            playerAudioData.PlayOneShot(swordclip, 0.5f);
            StartCoroutine(setSword());
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetBool("Attack", false);
            // print("PlayerStopAttack");
        }
        
    }

    IEnumerator setJumped()
    {
        yield return new WaitForSeconds(0.5f);
        //print("SET JUMP CO ROUTINE");
        jumped = true;
        
    }

    IEnumerator setSword()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        
    }


    void CheckCollision()
    {
        //print("check collision");
        //RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, enemyLayer);
        RaycastHit2D SwordHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, enemyLayer);

        if (SwordHit)
        {
            //print("hit skeleton!!!");
            if (SwordHit.collider.gameObject.tag == MyTags.SKELETON_TAG)
            {
                //print("hit skeleton!!!");
               // SwordHit.collider.gameObject.GetComponent<SkeletonScriptNew>().StunSkel();
                //SwordHit.collider.gameObject.GetComponent<SkeletonScriptNew>().DealDamage();
                //print(SwordHit.collider.gameObject.GetComponent<SkeletonScriptNew>().health--);

            }
        }

    }


    //only gets called when player enters, need to be called if players stays within range
    private void OnTriggerEnter2D(Collider2D collision){
        
        //if player collides with skelly weapon
        if (collision.gameObject.CompareTag("EnemyWeapon"))
        {
            
            if (canDamage)                
            {
                DamagePlayer(collision);                          
                             
                StartCoroutine(WaitForDamage());
            }
            else
            {
                print("SKELLY HIT BUT CANNOT DAMAGE PLAYER YET!");
            }
        }
    }
  
    void DamagePlayer(Collider2D collision)
    {
        

        lifeScoreCount--;
        blood.Play();
        print("PLAYER HAS BEEN HIT!!");
        anim.Play("PlayerHit", 0, 0f);
        playerAudioData.PlayOneShot(ouchClip, 0.5f);
        //Push player away to left from enemy if hit   from right           
        if (transform.position.x < collision.gameObject.transform.parent.gameObject.GetComponent<Transform>().position.x)
        {
            //float X = collision.gameObject.GetComponent<Transform>().position.x - offset;
            //float Y = collision.gameObject.GetComponent<Transform>().position.y;
            //corotuine interpolates between first parameter over a set duration using a for loop            
            StartCoroutine(Flinch(1,"left"));
            
        }
        else if (transform.position.x > collision.gameObject.transform.parent.gameObject.GetComponent<Transform>().position.x)
        {
            //transform.position = new Vector2(collision.gameObject.GetComponent<Transform>().position.x + offset, collision.gameObject.GetComponent<Transform>().position.y);
            StartCoroutine(Flinch(1,"right"));

        }


        //check life score to determine what happens to player
        if (lifeScoreCount >= 0)
        {
            lifeText.text = "x" + lifeScoreCount;
        }
        if (lifeScoreCount == 0)
        {

            anim.Play("PlayerDeath", 0, 0f);
            StartCoroutine(RestartGame());
        }
        canDamage = false;
    }

    IEnumerator Flinch(float X, string direction)
    {
        print("FLINCH IS RUNNING");


        if(direction == "left")
        {
            for (int i = 0; i <= X; i++)
            {
                //transform.position = new Vector2(X,Y);
                print("Player is " + transform.position.x);
                print("X is" + X);
                transform.position = new Vector2(transform.position.x - i, transform.position.y);
                yield return new WaitForSeconds(0.175f);
            }
        }   


        if (direction == "right")
        {
            for (int i = 0; i <= X; i++)
            {
                //transform.position = new Vector2(X,Y);
                print("Player is " + transform.position.x);
                print("X is" + X);
                transform.position = new Vector2(transform.position.x + i, transform.position.y);
                yield return new WaitForSeconds(0.175f);
            }
        }
            
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(-5, 0f, 0f), movespeed * Time.deltaTime);
    }





    IEnumerator WaitForDamage()
    {
        
        yield return new WaitForSeconds(0.5f);
        canDamage = true;
        blood.Stop();
    }

    IEnumerator RestartGame()
    {
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Gameplay");
    }







}////class


















