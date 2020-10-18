using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class bonusBlockScript : MonoBehaviour
{
    public LayerMask playerLayer;
    public Transform bottomCollision;
    private Animator anim;
    // Start is called before the first frame update
    public AudioClip clip;

    void Start()
    {
        anim = GetComponent<Animator>();
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
                    AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
                    anim.Play("BonusBlockIdle");
                    
                    //gameObject.transform.position = new Vector2(transform.position.x + 1,
                    //transform.position.y + 1);
                }
            }


        }
    }
}
