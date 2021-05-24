using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController4a : MonoBehaviour, EnemyController
{
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

    // Damage Sound Effect
    public AudioSource audio;

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

        if (audio == null){
            audio = GetComponent<AudioSource>();
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
                    fireSpinAttack.GetComponent<FireSpin4aScript>().ActivateAttack();
                    attackActivated = true;
                    attacksPerformed += 1;
                }
                else if (attackActivated == true)
                {
                    fireSpinAttack.GetComponent<FireSpin4aScript>().StartAttack();
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
    public string GetPhrase(int phraseNum)
    {
        if (phraseNum == 1) { return phrase1; }
        else if (phraseNum == 2) { return phrase2; }
        else if (phraseNum == 3) { return phrase3; }
        else if (phraseNum == 4) { return phrase4; }
        else if (phraseNum == 5) { return phrase5; }
        else if (phraseNum == 6) { return phrase6; }
        else if (phraseNum == 7) { return phrase7; }
        else { return "I'm speechless (literally)!"; }
    }


    //Health Related Methods:
    public void SetCurrentHealth(float health) { currentHealth = health; }
    public float GetCurrentHealth() { return currentHealth; }

    public void DamageTaken(bool truthValue) { tookDamage = true; }

    IEnumerator DamageCoroutine()
    {
        // Play Damage Sound Effect
        AudioSource.PlayClipAtPoint(audio.clip, transform.position);
        
        //Change Enemy color to red
        AdjustHealthBar();
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.18f);
        GetComponent<SpriteRenderer>().color = enemyColor;
        tookDamage = false;
    }

    //Resource Related Methods:
    public void AdjustHealthBar()
    {
        float healthRatio = currentHealth / maxHealth;
        if (healthBar.tag == "ResourceBar")
        {
            if (healthRatio == 1)
            {
                healthBar.transform.localScale = hbarScale;
            }
            else if (healthRatio < 1 && healthRatio >= 0)
            {
                Vector3 barScale = hbarScale; //Enemy maximum health (represented by bar)
                healthRatio = barScale.x * healthRatio; //Enemy remaining health (represented by bar)
                float lostHealth = barScale.x - healthRatio;
                Vector3 adjustment = new Vector3(lostHealth, 0, 0);
                healthBar.transform.localScale = hbarScale - adjustment;
            }
            else if (healthRatio < 0)
            {
                healthBar.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    public void AdjustEnergyBar()
    {
        float energyRatio = currentEnergy / maxEnergy;
        if (energyBar.tag == "ResourceBar")
        {
            if (energyRatio == 1)
            {
                energyBar.transform.localScale = ebarScale;
            }
            else if (energyRatio < 1 && energyRatio >= 0)
            {
                Vector3 barScale = ebarScale; //Enemy maximum energy (represented by bar)
                energyRatio = barScale.x * energyRatio; //Enemy remaining energy (represented by bar)
                float lostEnergy = barScale.x - energyRatio;
                Vector3 adjustment = new Vector3(lostEnergy, 0, 0);
                energyBar.transform.localScale = ebarScale - adjustment;
            }
        }
    }


    //Attack Related Methods:
    public void AttackPlayer() { isAttacking = true; }

    public bool AttackHasEnded() { return endedAttack; }

    public float GetAttacksPerformed() { return attacksPerformed; }
    public float GetAttacksMissed() { return attacksMissed; }

    public void IncrementAttacksLanded() { attacksLanded += 1; }
    public void CalculateAttacksMissed() { attacksMissed = attacksPerformed - attacksLanded; }

    IEnumerator WaitForAttackEndCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        endedAttack = true;
        CalculateAttacksMissed();
    }

    IEnumerator ResetEndedAttackCoroutine()
    {
        yield return new WaitForSeconds(5.5f);
        endedAttack = false;
    }


    //Emote Related Methods:
    public void StartEmote(string emoteName)
    {
        //Display emote bubble based on "emoteName" given
    }

    public void EndEmote(string emoteName)
    {
        //Hide emote bubble based on "emoteName" given
    }


}
