using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject fireBullet;
    private bool allowfire = true;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J) && allowfire)
        {
            StartCoroutine(ShootBullet());

        }
        
    }

    IEnumerator ShootBullet()
    {
        allowfire = false;
        GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
        bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        yield return new WaitForSeconds(0.5f);
        allowfire = true;
    }

}//class
















