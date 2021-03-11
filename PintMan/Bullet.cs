using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float moveSpeed = 10f;

	Rigidbody2D rb;
	PlayerMove target;
	Vector2 moveDirection;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindObjectOfType<PlayerMove>();
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
		//Destroy(gameObject, 3f);
		StartCoroutine(DestroyBullets());
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		print("Hit Player");
		if (collision.gameObject.tag == "Player")
        {
			print("Hit Player");
        }
    }


	IEnumerator DestroyBullets()
	{
		yield return new WaitForSeconds(3f);
		Destroy(this.gameObject);
		print("Bullet Destroyed");
	}
}
