using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{

    Rigidbody2D rb;
    float dirX, dirY;
    float torque;




    // Start is called before the first frame update
    void Start()
    {
        dirX = Random.Range(-5, 5);
        dirY = Random.Range(-5, 5);
        torque = Random.Range(-5, 5);
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(dirX, dirY), ForceMode2D.Impulse);
        rb.AddTorque(torque, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
