using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{

    
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Attacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hit by player Sword");

        if (collision.gameObject.CompareTag("Player"))
        {
            
            //parentPrefab.gameObject.GetComponent<FixedSkeleton>().canWalk = false;
            //parentPrefab.gameObject.GetComponent<FixedSkeleton>().attacking = true;
            //parentPrefab.gameObject.GetComponent<FixedSkeleton>().Attack();
        }
    }

    public void Attack()
    {
        print("ATTTACKING!!!");
        anim.SetBool("Attacking", true);
        //anim.Play("DemonAttack");
    }


    public void Idle()
    {
        print("IDLE!!!");
        anim.SetBool("Attacking", false);
    }






}//class














