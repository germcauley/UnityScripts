using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardaCarDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform parentPrefab;
    AudioSource carAudio;
    public AudioClip CarClip;



    // Start is called before the first frame update
    void Start()
    {
        parentPrefab = this.transform.parent;
        carAudio = parentPrefab.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            print("PLayer is in Range, play sound");
            carAudio.PlayOneShot(CarClip, 0.5f);
           
        }

    }
}
