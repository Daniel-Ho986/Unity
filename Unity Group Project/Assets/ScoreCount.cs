using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int score = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementScore(int amount)
    {
        if (amount < 0)
            Debug.Log("Invalid; amount may not be less than zero.");
        else
            score += amount;
    }

    public void IncrementScore()
    {
        IncrementScore(1);
    }
}
