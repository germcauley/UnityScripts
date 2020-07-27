using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject FireBullet;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootBullet()
    {
        if(Input.GetKey(KeyCode.J))
        {
            GameObject bullet = Instantiate(FireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }
}
