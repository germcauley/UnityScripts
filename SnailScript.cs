using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snailscript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    public LayerMask playerLayer;
    private bool moveLeft;

    private bool canMove;
    private bool stunned;

    public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    private Vector3 left_Collision_Position, right_Collision_Position;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left_Collision_Position = left_Collision.position;
        right_Collision_Position = right_Collision.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

   
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
             if (moveLeft){
                 myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);

                left_Collision.position = left_Collision_Position;
                right_Collision.position = right_Collision_Position;


            }
            else
            {
                myBody.velocity = new Vector2 (moveSpeed, myBody.velocity.y);
                left_Collision.position = right_Collision_Position;
                right_Collision.position = left_Collision_Position;
            }
        }
       
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if(topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    myBody.velocity = new Vector2 (0, 0);

                    anim.Play("Stunned");
                    stunned = true;
                    print("stunn snail");

                    //beetle code here
                    if(tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine ( Dead (3f) );
                    }

                }
            }
        }

        if (leftHit)
        {
            if(leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //APPLY DAMAGE TO PLAYER
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                    print("LEFT HIT!!");
                }
                else
                {
                    if(tag != MyTags.BEETLE_TAG)
                    {
                      myBody.velocity = new Vector2(15f, myBody.velocity.y);
                      StartCoroutine(Dead(3f));

                    }
                }
            }
        }

        if (rightHit)
        {
            if(rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //APPLY DAMAGE TO PLAYER
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                    print("RIGHT HIT!!");
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        //if collision not detected, do the following
        if (!Physics2D.Raycast (down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft; //change move left to oppositie of current value

        Vector3 tempScale = transform.localScale;

        if (moveLeft)//change direction of sprite
        {
            tempScale.x = Mathf.Abs(tempScale.x);
         
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

        }

        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }


    //SCRIPT FOR BULLETS HIT ENEMIES
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag != "MainCamera")
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }

            if (tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    print("stunning!!!");
                    print(target.tag);
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
       
    }






}//class
