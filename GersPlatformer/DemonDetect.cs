using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDetect : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player"))
        {
                
           
            parentPrefab.gameObject.GetComponent<DemonScript>().Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            //call the attack function this time it will reset as attacking is set to false           
            parentPrefab.gameObject.GetComponent<DemonScript>().Idle();
           
        }
    }











}///class













