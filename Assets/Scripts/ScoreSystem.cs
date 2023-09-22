using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float scoreMuiltiplier;
    float score;

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * scoreMuiltiplier;
        scoreText.text= "Score: " + Mathf.FloorToInt( score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighscore = PlayerPrefs.GetInt("HighScore");
        if (score > currentHighscore)
        {
            PlayerPrefs.SetInt("HighScore", Mathf.FloorToInt(score));
        }
    }
}
