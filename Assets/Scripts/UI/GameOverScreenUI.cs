using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverScreenUI : MonoBehaviour
{
    public TextMeshProUGUI UIHighScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetHighScore() 
    {

        if (UIHighScore != null)
        {
            if (GameManager.instance != null)
            {

                UIHighScore.text = "" + GameManager.instance.highscore;
            }
        }
    }
}
