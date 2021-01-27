using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    private bool move = true;
    public float moveSpeed = 1f;
    public bool Vertical, Horizontal;



    void Update()
    {

        if (Vertical)
        {
            // Use this for initialization
            if (move)
            {

                transform.Translate(0, 2 * Time.deltaTime * moveSpeed, 0);
            }

            else if (!move)
            {
                transform.Translate(0, -2 * Time.deltaTime * moveSpeed, 0);

            }
        }

        if (Horizontal)
        {
            // Use this for initialization
            if (move)
            {

                transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);

            }

            else if (!move)
            {
                transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);

            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {

            if (move)
            {
                move = false;

            }
            else
            {
                move = true;

            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {

            if (move)
            {
                move = false;

            }
            else
            {
                move = true;

            }
        }
    }
}
