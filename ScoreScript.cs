using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    private Text coinTextScore;
    private int scoreCount;

    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
      
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == MyTags.COIN_TAG)
        {
            print("cointouch!");
            scoreCount++;
            coinTextScore.text = "x" + scoreCount;
        }

       

    }

    public void BonusBlock()
    {
        print("Bonus score!");
        scoreCount += 5;
        coinTextScore.text = "x" + scoreCount;
    }
    

}//class















