using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator anim;
    private bool canMove = true;
    public GameObject Hitfx;
    public AudioClip ExplodeClip;
    AudioSource spudAudioData;
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.Play("PotatoIdle");
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Explode", false);
        StartCoroutine(DisableBullet(5f));
        spudAudioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        
        if(target.name == "GardaGun")
        {           
            canMove = false;            
            StartCoroutine(BulletHitAnim(target));            
        }
    }


    IEnumerator BulletHitAnim(Collider2D collision)
    {
        print("Play bullet hit anim");        
        anim.SetBool("Explode", true);
        spudAudioData.PlayOneShot(ExplodeClip, 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
      
    }



}//class









