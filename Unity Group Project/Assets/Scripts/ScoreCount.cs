using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] Text health;
    [SerializeField] Text damage;
    [SerializeField] Text coin;
    [SerializeField] Text score;
    // Start is called before the first frame update
    void Start()
    {
        //if (PersistentData.Instance != null){
        //}
            coin.text = PersistentData.Instance.GetCoin().ToString();
            displayHealth();
            displayDamage();
            displayCoin();
            displayScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (PersistentData.Instance != null){
        //}
            displayHealth();
            displayDamage();
            displayCoin();
            displayScore();
        
    }

    public void IncrementScore(int amount)
    {
        if (amount < 0)
            Debug.Log("Invalid; amount may not be less than zero.");
        else
            PersistentData.Instance.SetCoin(PersistentData.Instance.GetCoin() + amount);
    }

    public void IncrementScore()
    {
        IncrementScore(1);
    }

    public void displayHealth()
    {
        
        health.text = "Health: " + PersistentData.Instance.GetHealth();
    }
    public void displayDamage()
    {
        damage.text = "Damage: " + PersistentData.Instance.GetDamage();
    }
    public void displayCoin()
    {
        coin.text = "Coin: " + PersistentData.Instance.GetCoin();
    }
    public void displayScore()
    {
        score.text = "Score: " + PersistentData.Instance.GetScore();
    }
}
