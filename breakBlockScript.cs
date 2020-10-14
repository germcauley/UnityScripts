using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBlockScript : MonoBehaviour
{


    public LayerMask playerLayer;
    public Transform bottomCollision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();

        void CheckCollision()
        {
            

            Collider2D topHit = Physics2D.OverlapCircle(bottomCollision.position, 0.2f, playerLayer);

            if (topHit != null)
            {
                if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
                {
                    print("BREAK BLOCK!!!");
                }
            }

           
        }
    }
}