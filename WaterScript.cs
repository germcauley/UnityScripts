using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //use trigger as it allows player to fall though
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == MyTags.PLAYER_TAG)
        {
            print("Water death");
        }
    }
}
