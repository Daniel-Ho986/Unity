using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewHighScore : MonoBehaviour
{
    [SerializeField] public Text[] nameTexts;
    [SerializeField] public Text[] scoreTexts;

    public const string NAME_KEY = "HSName";
    public const string SCORE_KEY = "HSScore";

    public const int NUM_HIGH_SCORES = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            nameTexts[i].text = PlayerPrefs.GetString(NAME_KEY + i);
            scoreTexts[i].text = PlayerPrefs.GetInt(SCORE_KEY + i).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
