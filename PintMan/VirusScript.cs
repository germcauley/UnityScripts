using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusScript : MonoBehaviour
{
    private bool moveUp = true;    
    public float moveSpeed = 1f;
      

   
    void Update()
    {

        // Use this for initialization
        if (moveUp)
        {
            //determines movement of object move Y axis only in this case
            transform.Translate(0, 2 * Time.deltaTime * moveSpeed, 0);
            //transform.localScale = new Vector2(6, 6);
            
        }

        else if (!moveUp)
        {
            transform.Translate(0, -2 * Time.deltaTime * moveSpeed, 0);
            //transform.localScale = new Vector2(-6, 6);
            
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("patrol point!");
        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {

            if (moveUp)
            {
                moveUp = false;

            }
            else
            {
                moveUp = true;

            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("patrol point!");
        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {

            if (moveUp)
            {
                moveUp = false;

            }
            else
            {
                moveUp = true;

            }
        }

    }








    }//class
