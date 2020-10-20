using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{

    private Text lifeText;
    private int lifeScoreCount;
    private bool canDamage;


    // Start is called before the first frame update
    void Awake()
    {
        lifeText = GameObject.Find("LivesText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;
        canDamage = true;
    }

    // Update is called once per frame
    public void DealDamage()
    {
        if (canDamage)
        {
            lifeScoreCount--;
            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }
            if(lifeScoreCount == 0)
            {

            }
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
     
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }






}//class
