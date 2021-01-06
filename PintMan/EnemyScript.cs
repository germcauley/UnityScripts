using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public Transform EnemyHeadCollider;
    public LayerMask Player;
    private bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckIfHit();

        if (Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.5f, Player))
        {
            //print("Collided with enemy");
        }
    }


    void CheckIfHit()
    {
        isHit = Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.1f, Player);

        //print("Collided with enemy");

        if (isHit)
        {
            
           //print("Collided with enemy");
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {        
        if (coll.gameObject.tag == "Player")
            print("hit enemy");

    }





}//class
