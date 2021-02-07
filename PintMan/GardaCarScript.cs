using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardaCarScript : MonoBehaviour
{
    private bool move = true;
    public float moveSpeed = 1f;
    public bool Horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Horizontal)
        {
            // Use this for initialization
            if (move)
            {
                transform.localScale = new Vector2(1.2f, 1.2f);
                transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);

            }

            else if (!move)
            {
                transform.localScale = new Vector2(-1.2f, 1.2f);
                transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {
            print("hit patrol point");
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





}// class
