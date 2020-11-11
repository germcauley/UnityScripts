using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection_script : MonoBehaviour
{
    public Transform parentPrefab;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        parentPrefab = this.transform.parent;
        anim = parentPrefab.gameObject.GetComponent<Animator>();
        anim.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //private void OnColliderEnter2D(Collider2D target)
    //{
    //    if (target.gameObject.CompareTag("Ground"))
    //    {
    //        parentPrefab.gameObject.GetComponent<FixedSkeleton>().Attack();
    //        //anim.SetBool("Attack", true);
    //        //StartCoroutine(ResetIdle());
    //    }
       
    //}

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLayer is in Range, ATTACK!");
            parentPrefab.gameObject.GetComponent<FixedSkeleton>().canWalk = false;
            parentPrefab.gameObject.GetComponent<FixedSkeleton>().attacking = true;            
            parentPrefab.gameObject.GetComponent<FixedSkeleton>().Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("PLayer is out of RANGE");
            //call the attack function this time it will reset as attacking is set to false
            parentPrefab.gameObject.GetComponent<FixedSkeleton>().attacking = false;
            parentPrefab.gameObject.GetComponent<FixedSkeleton>().Attack();
            //print(parentPrefab.gameObject.GetComponent<FixedSkeleton>().attacking);
        }
    }














}///class
