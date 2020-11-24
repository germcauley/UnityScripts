using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{

    
    private Animator anim;
    private GameObject Fireball;
    // Start is called before the first frame update



    private void Awake()
    {
        Fireball = gameObject.transform.GetChild(1).gameObject;
        Fireball.SetActive(false);
    }
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

    public void FireOn()
    {
        Fireball.SetActive(true);
        print("fire on!");
    }

    public void FireOff()
    {
        Fireball.SetActive(false);
        print("fire off!");
    }






}//class














