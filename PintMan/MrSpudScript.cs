using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class MrSpudScript : MonoBehaviour
{
    public GameObject bagOfSpuds, Pintman;
    public Transform attackInstantiate;
    private Animator anim;
    private bool stunned = false,hit=false;
    public LayerMask playerLayer;
    private string coroutine_Name = "StartAttack";
    public float ProjectileSpeed, fireRate, nextFire;
    int bounceDir=100, randNum,mrSpudHealth;

    public AudioClip BossPainClip;
    private AudioSource BossAudioData;
    public Transform EnemyHeadCollider;
    private Rigidbody2D pintManRB;
    public Vector2 movement;
    private Renderer rend;
    [SerializeField]
    private Color colorToTurnTo = Color.white;

    //Object pooling code
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;




    void Start()
    {
        Pintman = GameObject.Find("PintMan");
        BossAudioData = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        pintManRB = Pintman.GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();

        //Add objects to pool
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        //end pooling code


    }


    void Update()
    {
        //CheckIfHit();
        CheckCollision();
        if (Physics2D.Raycast(EnemyHeadCollider.position, Vector2.up, 0.5f, playerLayer))
        {
            //print("Collided with enemy");
        }
        //Makes the Enemy sprite face the direction of the player character byt flipping x axis when required
        if (transform.position.x > Pintman.transform.position.x)
        {


            
            transform.localScale = new Vector2(2f, 2f);
        }
        else if (transform.position.x < Pintman.transform.position.x)
        {
           
            
            transform.localScale = new Vector2(-2f, 2f);
        }

       
        movement = new Vector2(bounceDir, 0f);

    }

    private void FixedUpdate()
    {
        if (hit)
        {
            moveCharacter(movement);
           
        }
    }

    void moveCharacter(Vector2 direction)
    {
        pintManRB.AddForce(direction * 3f);

    }


    void Attack()
    {

       // GameObject obj = Instantiate(bagOfSpuds, attackInstantiate.position, Quaternion.identity);
       /// obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700), 0f));

        Instantiate(bagOfSpuds, transform.position, Quaternion.identity);
        bagOfSpuds.GetComponent<BagOfSpudsScript>().moveSpeed = ProjectileSpeed;
    }

    void BackToIdle()
    {
        anim.Play("BossAnimation");
    }

    //check if player has jumped on head
    void CheckCollision()
    {


        Collider2D topHit = Physics2D.OverlapCircle(EnemyHeadCollider.position, 0.5f, playerLayer);
        //if player jumps on enemy head it can stun or kill
        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {             
                if (!stunned)
                {
                    randNum = Random.Range(0, 2);                    
                    if (randNum == 0)
                    {
                        bounceDir = 100;
                    }
                    else
                    {
                        bounceDir = -100;
                    }
                    stunned = true;
                    hit = true;
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);                     
                    
                    StartCoroutine(MovePlayer());
                    StartCoroutine(DamageColour());
                    StartCoroutine(Stunned());                   
                }
            }
        }
    }

    //public GameObject GetPooledObject()
    //{
    //    //1
    //    for (int i = 0; i < pooledObjects.Count; i++)
    //    {
    //        //2
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }
    //    //3   
    //    return null;
    //}

//Coroutines

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("BossAttack");
        StartCoroutine(coroutine_Name);
    }

    IEnumerator MovePlayer()
    {
        
        yield return new WaitForSeconds(.5f);
        hit = false;
    }


    IEnumerator Stunned()
    {
        BossAudioData.PlayOneShot(BossPainClip, 0.8f);
        anim.SetBool("Stunned", true);
        //make crisps appear when mr spud hit


        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = new Vector3(42f+i, -2f, 0f);
                //bullet.transform.rotation = new Vector3(42f, -2f, 0f)
                pooledObjects[i].SetActive(true);
            }
        }
       

        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Stunned", false);
        stunned = false;                
    }

    IEnumerator DamageColour()
    {
        rend.material.color = colorToTurnTo;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.white;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = colorToTurnTo;
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material.color = Color.white;
    }









}//end of class
