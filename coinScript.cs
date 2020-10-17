using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class coinScript : MonoBehaviour
{


    public AudioClip clip;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == MyTags.PLAYER_TAG)
        {
            print("COIN COLLECT!!!!");

            AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));

            Destroy(gameObject);
            
        }
    }
}
