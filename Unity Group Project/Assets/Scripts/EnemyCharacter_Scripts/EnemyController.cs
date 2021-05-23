using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyController
{
    /*
    //These are set in the editor
    public GameObject healthBar;
    private Vector3 hbarScale;
    public GameObject energyBar;
    private Vector3 ebarScale;

    //These are set in the editor (for now)
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float currentEnergy;
    [SerializeField] float maxEnergy;
    //[SerializeField] float movementSpeed;
    //[SerializeField] float weight;

    //Resource variables:
    public bool tookDamage;
    public bool usedEnergy;

    public Rigidbody2D rigid;
    public bool facingLeft;
    public bool facingRight;

    //Attack Variables:
    public GameObject fireSpinAttack;
    public GameObject fireSpinAttack2;

    public bool isAttacking;
    public bool attackActivated;
    public bool endedAttack;

    public float attacksPerformed;
    public float attacksLanded;
    public float attacksMissed;

    //Appearance Variables:
    public Color enemyColor;

    //Speech Variables:
    private string phrase1;
    private string phrase2;
    private string phrase3;
    private string phrase4;
    private string phrase5;
    private string phrase6;
    private string phrase7;


    // Start is called before the first frame update
    void Start()
    {
        if (healthBar != null && healthBar.tag == "ResourceBar")
        {
            hbarScale = healthBar.transform.localScale;
        }

        if (energyBar != null && energyBar.tag == "ResourceBar")
        {
            ebarScale = energyBar.transform.localScale;
        }

        tookDamage = false;
        usedEnergy = false;

        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        facingLeft = true;
        facingRight = false;
        enemyColor = GetComponent<SpriteRenderer>().color;

        isAttacking = false;
        attackActivated = false;
        endedAttack = false;

        attacksPerformed = 0.0f;
        attacksMissed = 0.0f;

        phrase1 = "Don't you go touching my data!!!";
        phrase2 = "You're messing up my data >:(";
        phrase3 = "No, no, NO!!! Hands off I say!!!";
        phrase4 = "My stuff!!! I can't believe it >:(";
        phrase5 = "Everything was fine 'till YOU showed up!";
        phrase6 = "I'm gonna get scrapped for this...*sad boop beep*";
        phrase7 = "That somehow looks better... *beep boop*";
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy is attacked--
        if (tookDamage)
        {
            StartCoroutine(DamageCoroutine());
        }

        //Enemy uses energy for abilities--
        if (usedEnergy)
        {
            AdjustEnergyBar();
            usedEnergy = false;
        }

        if (isAttacking)
        {
            endedAttack = false;
            if (fireSpinAttack != null)
            {
                if (attackActivated == false)
                {
                    if(currentHealth <= maxHealth / 2){
                        fireSpinAttack2.GetComponent<FireSpinScript>().ActivateAttack();
                    } else 
                    {
                        fireSpinAttack.GetComponent<FireSpinScript>().ActivateAttack();
                    }
                    attackActivated = true;
                    attacksPerformed += 1;
                }
                else if (attackActivated == true)
                {
                    if(currentHealth <= maxHealth / 2){
                        fireSpinAttack2.GetComponent<FireSpinScript>().StartAttack();
                    } else 
                    {
                        fireSpinAttack.GetComponent<FireSpinScript>().StartAttack();
                    }
                    isAttacking = false;
                    attackActivated = false;
                    StartCoroutine(WaitForAttackEndCoroutine());
                    StartCoroutine(ResetEndedAttackCoroutine());
                }

            }
        }
    }//End of Update()



    //GetPhrase():
    //  - returns the enemy phrase depending on the number entered
    string GetPhrase(int phraseNum);


    //Health Related Methods:
    void SetCurrentHealth(float health);
    float GetCurrentHealth();
    void DamageTaken(bool truthValue);
    //IEnumerator DamageCoroutine();


    //Resource Related Methods:
    void AdjustHealthBar();
    void AdjustEnergyBar();


    //Attack Related Methods:
    void AttackPlayer();
    bool AttackHasEnded();
    float GetAttacksPerformed();
    float GetAttacksMissed();
    void IncrementAttacksLanded();
    void CalculateAttacksMissed();
    //IEnumerator WaitForAttackEndCoroutine();
    //IEnumerator ResetEndedAttackCoroutine();


    //Emote Related Methods:
    void StartEmote(string emoteName);
    void EndEmote(string emoteName);

}
