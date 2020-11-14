using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerScoreLivesScript : MonoBehaviour
{

    private Text lifeText;
    private int lifeScoreCount;
    // Start is called before the first frame update
    void Start()
    {
        lifeText = GameObject.Find("LivesText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;
        Debug.Log(lifeText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
