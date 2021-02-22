using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 CurrentPos;
    Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        CurrentPos = gameObject.transform.position;
        print("Current po is : " + CurrentPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("PintMan"))
        {
            StartCoroutine(DropPlatform());
            //Destroy(gameObject, 2f);
  
        }
        
    }

    IEnumerator DropPlatform()
    {
        print(transform.position);
        print("drop platform");
        rb.isKinematic = false;        
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        rb.WakeUp();
        rb.isKinematic = true;
        print(rb.isKinematic);
        yield return new WaitForSeconds(1);
        transform.position = originalPos;
        rb.velocity = CurrentPos;
    }

 

}
