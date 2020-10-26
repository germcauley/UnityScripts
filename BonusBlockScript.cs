using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class bonusBlockScript : MonoBehaviour
{
    public LayerMask playerLayer;
    public Transform bottomCollision;
    private Animator anim;
    // Start is called before the first frame update
    public AudioClip clip;
    

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
  
    private bool startAnim;
    private bool canAnimate = true;
    //private Text coinTextScore;
    //private int scoreCount;

    void Awake()
    {
        anim = GetComponent<Animator>();
      
    }

    void Start()
    {
        
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        AnimateUpDown();
        
    }
    void CheckCollision()
    {
        if (canAnimate)
        {
            //Collider2D topHit = Physics2D.OverlapCircle(bottomCollision.position, 0.2f, playerLayer);
            RaycastHit2D topHit = Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, playerLayer);
            if (topHit)
            {
                if (topHit.collider.gameObject.tag == MyTags.PLAYER_TAG && canAnimate)
                {
                    //increase score                
                    AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
                    anim.Play("BonusBlockIdle");
                    print("BONUS BLOCK ACTIVATED!");
                    //scoreCount += 5;
                    //coinTextScore.text = "x" + scoreCount;
                    //print("BONUS BLOCK CHANGED!");
                    startAnim = true;
                    canAnimate = false;                   
                }
            }
        }
        


    }

    void AnimateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if (transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if (transform.position.y <= originPosition.y)
            {
                startAnim = false;
            }
        }
    }
}
