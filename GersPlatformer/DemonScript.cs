using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
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

    void Attack()
    {
        
    }







}//class














