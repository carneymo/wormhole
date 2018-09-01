using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    
    void UpdateScoreText(Text scoreText, int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
