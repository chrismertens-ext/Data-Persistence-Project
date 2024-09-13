using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    // Start is called before the first frame update
    void Start()
    {
        MainManager.instance.ScoreText = ScoreText.GetComponent<Text>();
        MainManager.instance.HighScoreText = HighScoreText.GetComponent<Text>();
        MainManager.instance.GameOverText = GameOverText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
