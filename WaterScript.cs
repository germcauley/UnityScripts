using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class WaterScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip clip;
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
            print("Water death");
            AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
        }
    }
}
