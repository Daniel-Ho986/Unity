using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PersistentData : MonoBehaviour
{
    [SerializeField] public string playerName;
    [SerializeField] int playerHealth;
    [SerializeField] int playerDamage;
    [SerializeField] List<string> playerAbility = new List<string>();
    [SerializeField] int playerCoin;
    [SerializeField] int playerScore;

    public static PersistentData Instance;
    // Start is called before the first frame update
    void Start()
    {
        playerName = "";
        playerHealth = 10;
        playerDamage = 1;
        playerCoin = 0;
        playerScore = 0;
    }
    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetName(string name)
    {
        playerName = name;
    }
    public void SetHealth(int health)
    {
        playerHealth = health;
    }
    public void SetDamage(int damage)
    {
        playerDamage = damage;
    }
    public void SetCoin(int coin)
    {
        playerCoin = coin;
    }
    public void SetScore(int score)
    {
        playerScore = score;
    }
    public void AddAbility(string ability)
    {
        playerAbility.Add(ability);
    }
    public void DeleteAbility(string ability)
    {
        playerAbility.Remove(ability);
    }


    public string GetName()
    {
       return playerName;
    }
    public int GetHealth()
    {
        return playerHealth;
    }
    public int GetDamage()
    {
        return playerDamage;
    }
    public int GetCoin()
    {
        return playerCoin;
    }
    public int GetScore()
    {
        return playerScore;
    }
    public string[] GetAbility()
    {
        return playerAbility.ToArray();
    }




    // Update is called once per frame
    void Update()
    {
        playerScore = 20 * playerHealth + 10 * playerCoin;

        if(playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("WIP");
    }
}
