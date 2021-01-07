using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public Transform EnemyHeadCollider;
    public LayerMask playerLayer;
     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //CheckIfHit();
        CheckCollision();
        if (Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.5f, playerLayer))
        {
            //print("Collided with enemy");
        }
    }


    //void CheckIfHit()
    //{
    //    isHit = Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.1f, playerLayer);

    //    //print("Collided with enemy");

    //    if (isHit)
    //    {
            
    //       print("Collided with enemy");
    //    }
    //}



   // check for player collision using raycast
    void CheckCollision()
    {
        

        //RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        //RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.2f, playerLayer);
        print(topHit.gameObject.name);
        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                print("Enemy hit!!!");
                //if (!stunned)
                //{
                topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                //    canMove = false;
                //    myBody.velocity = new Vector2(0, 0);

                //    anim.Play("Stunned");
                //    stunned = true;

                //    // BEETLE CODE HERE
                //    if (tag == MyTags.BEETLE_TAG)
                //    {
                //        anim.Play("Stunned");
                //        StartCoroutine(Dead(0.5f));
                //    }
                //}
            }          
        }
    }





    }//class
