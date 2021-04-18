using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    [SerializeField] float movementSpeed;
    [SerializeField] float weight;

    public bool tookDamage;
    public bool usedEnergy;

    public Vector2 movement;
    public float hMovement;
    public float vMovement;
    public float jumpForce = 20.0f;

    //These are set in the editor
    public Transform feet;
    public LayerMask groundLayers;

    //Initialized in Start()
    public Rigidbody2D rigid;
    public InputManager inputManager;

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

        movementSpeed = 10.0f;
        weight = 5.0f;

        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        inputManager = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (tookDamage)
        {
            AdjustHealthBar();
            tookDamage = false;
        }

        if (usedEnergy)
        {
            AdjustEnergyBar();
            usedEnergy = false;
        }

        hMovement = Input.GetAxisRaw("Horizontal");

        if ((inputManager.GetKeyDown(KeyBindingActions.Jump1)
            || inputManager.GetKeyDown(KeyBindingActions.Jump2)) && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        movement = new Vector2(hMovement * movementSpeed, rigid.velocity.y);
        rigid.velocity = movement;
    }

    private void Jump()
    {
        movement = new Vector2(rigid.velocity.x, jumpForce);
        rigid.velocity = movement;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }

        return false;
    }

    private void AdjustHealthBar()
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
                Vector3 barScale = hbarScale; //Player maximum health (represented by bar)
                healthRatio = barScale.x * healthRatio; //Player remaining health (represented by bar)
                float lostHealth = barScale.x - healthRatio;
                Vector3 adjustment = new Vector3(lostHealth, 0, 0);
                healthBar.transform.localScale = hbarScale - adjustment;
            }
            
        }
    }

    private void AdjustEnergyBar()
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
                Vector3 barScale = ebarScale; //Player maximum energy (represented by bar)
                energyRatio = barScale.x * energyRatio; //Player remaining energy (represented by bar)
                float lostEnergy = barScale.x - energyRatio;
                Vector3 adjustment = new Vector3(lostEnergy, 0, 0);
                energyBar.transform.localScale = ebarScale - adjustment;
            }
        }
    }
}
