using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour
{
    private bool moveLeft = true,canHit=true;
    public float moveSpeed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            //determines movement of object move X axis only in this case
            transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(2.2f, 2.2f);
            transform.rotation = Quaternion.Euler(-2, 0, -7.552f);

        }

        else if (!moveLeft)
        {
            transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(-2.2f, 2.2f);
            transform.rotation = Quaternion.Euler(2, 0, 7.552f);


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint")&& canHit)
        {
            canHit = false;
            StartCoroutine(SwitchDirection());
        }
    }

    IEnumerator SwitchDirection()
    {
        print("Switch direction!");

        if (moveLeft)
        {
            moveLeft = false;

        }
        else
        {
            moveLeft = true;

        }
        yield return new WaitForSeconds(2.0f);
        canHit = true;
    }




}//class
