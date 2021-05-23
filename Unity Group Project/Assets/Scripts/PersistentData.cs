using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PersistentData : MonoBehaviour
{
    [SerializeField] public string playerName;
    [SerializeField] int maxPlayerHealth;
    [SerializeField] int currentPlayerHealth;
    [SerializeField] int prevPlayerHealth;
    [SerializeField] int playerDamage;
    [SerializeField] List<string> playerAbility = new List<string>();
    [SerializeField] int playerCurrency;
    [SerializeField] int playerScore;

    public bool playerDefeated;
    public bool hasDisplayedGameOver;

    public static PersistentData Instance;
    // Start is called before the first frame update
    void Start()
    {
        playerName = "";
        maxPlayerHealth = 10;
        currentPlayerHealth = 10;
        prevPlayerHealth = 10;
        playerDamage = 1;
        playerCurrency = 0;
        playerScore = 0;

        playerDefeated = false;
        hasDisplayedGameOver = false;
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
    public void SetMaxHealth(int maxHealth)
    {
        maxPlayerHealth = maxHealth;
    }
    public void SetCurrentHealth(int currentHealth)
    {
        currentPlayerHealth = currentHealth;
    }
    public void SetPrevHealth(int prevHealth)
    {
        prevPlayerHealth = prevHealth;
    }
    public void SetDamage(int damage)
    {
        playerDamage = damage;
    }
    public void SetCurrency(int currency)
    {
        playerCurrency = currency;
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
    public int GetMaxHealth()
    {
        return maxPlayerHealth;
    }
    public int GetCurrentHealth()
    {
        return currentPlayerHealth;
    }
    public int GetPrevHealth()
    {
        return prevPlayerHealth;
    }
    public int GetDamage()
    {
        return playerDamage;
    }
    public int GetCurrency()
    {
        return playerCurrency;
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

        playerScore = 20 * currentPlayerHealth + 10 * playerCurrency;
        if(currentPlayerHealth < 0){
            currentPlayerHealth = 0;
        }

        if(currentPlayerHealth <= 0 && playerDefeated == false)
        {
            playerDefeated = true;
        }

    }

    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
