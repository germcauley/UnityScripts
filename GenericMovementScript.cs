using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GerPlayerMovement : MonoBehaviour
{

    public float MoveSpeed = 5f;

    float dirX, dirY;
    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        dirX = Input.GetAxis ("Horizontal")*moveSpeed;
        dirY = Input.GetAxis ("Vertical")*moveSpeed;
    }

    void FixedUpdate(){
        rb.velocity = new Vector2 (dirX,dirY);
    }
}//class


















