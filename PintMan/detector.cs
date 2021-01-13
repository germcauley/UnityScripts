using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public Transform parentPrefab;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        parentPrefab = this.transform.parent;
        anim = parentPrefab.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        print("PLayer is in DDD Range, ATTACK!");
    //        anim.SetBool("Attack",true);
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("PLayer is in DDD Range, ATTACK!");
            anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("PLayer is in DDD Range, ATTACK!");
            anim.SetBool("Attack", false);
        }
    }


}//class
