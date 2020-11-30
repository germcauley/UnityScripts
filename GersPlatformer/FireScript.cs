using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    Collider FireCollider;
    // Start is called before the first frame update
    void Start()
    {
        FireCollider = GetComponent<Collider>();
        //FireCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("somwthings burning!");
        }
    }

    public void ToggleOnFireCollider()
    {
        gameObject.SetActive(true);
    }
    public void ToggleOffFireCollider()
    {
        gameObject.SetActive(false);
    }





}//class
