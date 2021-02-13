using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject fireBullet;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }

}//class















