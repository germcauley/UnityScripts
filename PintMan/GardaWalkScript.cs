using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardaWalkScript : MonoBehaviour
{
    public Transform EnemyHeadCollider;
    public LayerMask playerLayer;
    private bool moveLeft = true;
    public float moveSpeed = 1f;
    private bool stunned = false;
    private GameObject HitFX;
    public AudioClip DeathClip;
    AudioSource enemyAudioData;




    void Start()
    {
        HitFX = gameObject.transform.GetChild(1).gameObject;       
        enemyAudioData = GetComponent<AudioSource>();
        HitFX.SetActive(false);
    }

   
    void Update()
    {

        CheckCollision();
        
        if (moveLeft)
        {
            //determines movement of object move X axis only in this case
            transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(-0.15f, 0.15f);
            transform.rotation = Quaternion.Euler(0, 0, -7.552f);

        }

        else if (!moveLeft)
        {
            transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
            transform.localScale = new Vector2(0.15f, 0.15f);
            transform.rotation = Quaternion.Euler(0, 0, 7.552f);          
            

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //patrol script
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {

            if (moveLeft)
            {
                moveLeft = false;

            }
            else
            {
                moveLeft = true;

            }
        }
    }

    void CheckCollision()
    {


        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {                

                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    StartCoroutine(DestroyEnemy());
                }
            }
        }
    }
    IEnumerator DestroyEnemy()
    {

        //enemyAudioData.PlayOneShot(DeathClip, 0.5f);
        HitFX.SetActive(true);
        //enemyAudioData.PlayOneShot(DeathClip, 0.1f);
        yield return new WaitForSecondsRealtime(0.7f);
        gameObject.SetActive(false);
    }





}//class
