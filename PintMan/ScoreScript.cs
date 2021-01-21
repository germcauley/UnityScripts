using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text PintTextScore;
    private int scoreCount =0;

    void Start()
    {
        PintTextScore = GameObject.Find("PintsText").GetComponent<Text>();

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == MyTags.PINT_TAG)
        {
            print("pintouch!");
            scoreCount++;
            print(scoreCount);
            PintTextScore.text = "x" + scoreCount;
        }



    }

    public void BonusBlock()
    {
        print("Bonus score!");
        scoreCount += 5;
        PintTextScore.text = "x" + scoreCount;
    }
}
