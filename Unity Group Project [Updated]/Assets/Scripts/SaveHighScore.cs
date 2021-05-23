using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHighScore : MonoBehaviour
{
    public const string NAME_KEY = "HSName";
    public const string SCORE_KEY = "HSScore";

    public const int NUM_HIGH_SCORES = 5;

    string playerName;
    int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();

        SaveScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SaveScore()
    {
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            string curNameKey = NAME_KEY + i;
            string curScoreKey = SCORE_KEY + i;

            if (!PlayerPrefs.HasKey(curScoreKey))
            {
                PlayerPrefs.SetInt(curScoreKey, playerScore);
                PlayerPrefs.SetString(curNameKey, playerName);
                return;
            }
            else
            {
                int score = PlayerPrefs.GetInt(curScoreKey);

                if (playerScore > score)
                {
                    int tempScore = score;
                    string tempName = PlayerPrefs.GetString(curNameKey);

                    PlayerPrefs.SetInt(curScoreKey, playerScore);
                    PlayerPrefs.SetString(curNameKey, playerName);

                    playerName = tempName;
                    playerScore = tempScore;

                }
            }

        }
    }
    public void ViewHighScores()
    {
        SceneManager.LoadScene("HighScore");
    }

}
