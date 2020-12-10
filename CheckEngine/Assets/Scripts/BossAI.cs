using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // Different stets the enemy can be in.
    private States currentState;
    enum States
    {
        Idle,
        Flying,
        Charge,
        Lightning,
        Rest,
        Death
    }

    // Modifyable fields.
    [SerializeField] private int health = 4;
    [SerializeField] private float speed = 3;
    [Header("Attacked")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private GameObject restPlace;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color originalColor;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    // Times Delay
    private float currentTime;
    [SerializeField] private float idleTimeDelay;
    [SerializeField] private float restTime;

    // Hidden variables.
    private bool playerFound = false;
    private float currentHealth;
    private float attackingStartTime;
    private float distanceFromPlayer;
    private bool groundDetected;

    // Get componenets.
    private Animator animator;
    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerFound = true;
            currentHealth = health;
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            if(groundDetected)
            {
                SwitchState(States.Idle);
            }
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (bodyParts.Length > 0)
            {
                originalColor = bodyParts[0].GetComponent<SpriteRenderer>().color;
            }
        }
    }

    private void Update()
    {
        if (playerFound)
        {
            distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            switch (currentState)
            {
                case States.Idle:
                    UpdateIdleState();
                    break;
                case States.Flying:
                    UpdateFlyingState();
                    break;
                case States.Charge:
                    UpdateChargeState();
                    break;
                case States.Rest:
                    UpdateRestState();
                    break;
                case States.Death:
                    UpdateDeathState();
                    break;
            }
        }
    }

    //-------- Idle ---------------------------------------
    private void EnterIdleState()
    {
        animator.SetBool("isLanded", true);
        currentTime = Time.time;
    }
    public void UpdateIdleState()
    {
        if (Time.time >= Time.time + currentTime)
        {
            SwitchState(States.Flying);
        }
    }
    public void ExitIdleState()
    {
        animator.SetBool("isLanded", false);
    }

    //-------- Flying ---------------------------------------
    private void EnterFlyingState()
    {
        animator.SetTrigger("isFlying");
        currentTime = Time.time;

    }
    public void UpdateFlyingState()
    {
        if (this.transform.transform.position.x != pointA.transform.position.x && this.transform.transform.position.y != pointA.transform.position.y)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, pointA.transform.position, Time.deltaTime);
        }
        else
        {
            if (Time.time >= Time.time + currentTime)
            {
                SwitchState(States.Flying);
            }
        }
    }
    public void ExitFlyingState()
    {

    }

    //-------- Charge ---------------------------------------
    private void EnterChargeState()
    {

    }
    public void UpdateChargeState()
    {

    }
    public void ExitChargeState()
    {

    }

    //-------- Lightning ---------------------------------------
    private void EnterLightningState()
    {

    }
    public void UpdateLightningState()
    {

    }
    public void ExitLightningState()
    {

    }

    //-------- Rest ---------------------------------------
    private void EnterRestState()
    {

    }
    public void UpdateRestState()
    {

    }
    public void ExitRestState()
    {

    }

    //-------- Death ---------------------------------------
    private void EnterDeathState()
    {

    }
    public void UpdateDeathState()
    {

    }
    public void ExitDeathState()
    {

    }


    //--------- Other Functions ----------------------------
    public void Damage(float[] attackDetails)
    {
        // Decrease the health
        currentHealth -= attackDetails[0];

        // Change color
        if (bodyParts.Length > 0)
        {
            StartCoroutine(flash());
        }

        if (currentHealth > 0.0f)
        {
            //SwitchState(States.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(States.Death);
        }
    }

    private void Flip()
    {

    }

    private void SwitchState(States state)
    {
        switch (currentState)
        {
            case States.Idle:
                ExitIdleState();
                break;
            case States.Flying:
                ExitFlyingState();
                break;
            case States.Charge:
                ExitChargeState();
                break;
            case States.Rest:
                ExitRestState();
                break;
            case States.Death:
                ExitDeathState();
                break;
        }

        switch (state)
        {
            case States.Idle:
                EnterIdleState();
                break;
            case States.Flying:
                EnterFlyingState();
                break;
            case States.Charge:
                EnterChargeState();
                break;
            case States.Rest:
                EnterRestState();
                break;
            case States.Death:
                EnterDeathState();
                break;
        }

        currentState = state;
    }

    IEnumerator flash()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = originalColor;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}