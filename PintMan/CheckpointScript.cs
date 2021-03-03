using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{


    private GameMaster gm;
    public AudioClip CheckPointClip;
    private AudioSource CheckPointAudio;
    private Animator myAnimator;

    void Start(){
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        CheckPointAudio = GetComponent<AudioSource>();     
        myAnimator = GetComponent<Animator>();
        myAnimator.GetComponent<Animator>().enabled = false;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
            CheckPointAudio.PlayOneShot(CheckPointClip, 1f);
            StartCoroutine(PlayCheckPointAnim());
        }

    }

    IEnumerator PlayCheckPointAnim()
    {
        myAnimator.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(.5f);

    }
        
        
       

}//end of class
