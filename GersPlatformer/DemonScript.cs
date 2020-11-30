using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{

    
    private Animator anim;
    private GameObject Fireball;
    public int health = 3;
    public AudioClip attackClip;
    public AudioClip ouchclip;
    AudioSource demonAudioData;
    public GameObject hitSprite;
    // Start is called before the first frame update



    private void Awake()
    {
        Fireball = gameObject.transform.GetChild(1).gameObject;
        Fireball.SetActive(false);
        demonAudioData = GetComponent<AudioSource>();
    }
    void Start()
    {
       
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Attacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    //NEED TO MOE THE PLAYER SWORD COLLIDER SIMILAR TO THE SKELETON AXE SO THAT IT ALLWAYS ENTERES THE DEMON COLLIDER OBJECT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Sword") && collision.gameObject.activeInHierarchy == true)
        {
            anim.SetBool("Attacking", false);

            //move player back slightly
            print("demon body collider");
            print("demon has been hit!!!!");
            demonAudioData.PlayOneShot(ouchclip, 0.5f);

            if (health == 0)
            {
                print("Demon dead");
            }
            else
            {
                health--;
            }
        }
    }

    public void Attack()
    {
        //print("ATTTACKING!!!");
        anim.SetBool("Attacking", true);
        
        //anim.Play("DemonAttack");

    }


    public void Idle()
    {
       // print("IDLE!!!");
        anim.SetBool("Attacking", false);
    }

    public void FireOn()
    {
        Fireball.SetActive(true);
        //print("fire on!");
        demonAudioData.PlayOneShot(attackClip, 0.5f);
    }

    public void FireOff()
    {
        Fireball.SetActive(false);
        //print("fire off!");
    }

    






}//class














