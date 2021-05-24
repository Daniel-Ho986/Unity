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
    */

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
