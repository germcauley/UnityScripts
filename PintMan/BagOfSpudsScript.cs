using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfSpudsScript : MonoBehaviour
{
	public float moveSpeed = 10f;

	Rigidbody2D rb;
	PlayerMove target;
	Vector2 moveDirection;
	private Animator anim;

	void Awake()
    {
		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start()
	{
		anim.SetBool("Exploded", false);
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindObjectOfType<PlayerMove>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
		//Destroy(gameObject, 3f);
	}


	private void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Ground")
		{
			StartCoroutine(SpudsExploded());
		}
	}

	IEnumerator SpudsExploded()
    {
		rb.velocity = new Vector2(0, 0);
		rb.gravityScale = 0;
		anim.SetBool("Exploded", true);

		yield return new WaitForSeconds(1.1f);
		print("Spuds exlpoded");
		Destroy(gameObject);
	}
}
