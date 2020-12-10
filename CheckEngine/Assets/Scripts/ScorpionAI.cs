using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAI : MonoBehaviour
{

    // Different stets the enemy can be in.
    private States currentState;
    enum States
    {
        Moving,
        Knockback,
        Attack,
        Death
    }

    // Modifiable fileds.
    [SerializeField] private float groundCheckDistance = 0.5f, wallCheckDistance = 0.5f, playerCheckDistance = 1.0f;
    [SerializeField] private float maxHealth, knockbackDuration = 0.2f, attackingDuration = 0.08f, movSpeed = 4.0f;
    [SerializeField] private Transform groundCheck, wallcheck, playerCheck, attackTrigger;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 knockbackSpeed;
    [Header("Attacked")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color originalColor;
    [Header("Attack")]
    [SerializeField] private GameObject spit;


    // Private variables.
    private bool groundDetected, wallDetected, playerDetected;
    private int facingDirection;
    private int damageDirection;
    private int facingRight;
    private float currentHealth;
    private float knockbackStartTime;
    private float attackingStartTime;

    // Need access to.
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private LayerMask whatIsPlayer;

    private void Start()
    {
        facingDirection = 1;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        whatIsPlayer = LayerMask.GetMask("Player");
        if (bodyParts.Length > 0)
        {
            originalColor = bodyParts[0].GetComponent<SpriteRenderer>().color;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case States.Moving:
                UpdateMovingState();
                break;
            case States.Knockback:
                UpdateKnockbackState();
                break;
            case States.Attack:
                UpdateAttackState();
                break;
        }
    }

    //---------Moving----------------------------
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallcheck.position, transform.right, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.Raycast(playerCheck.position, transform.right, playerCheckDistance, whatIsPlayer);
        if (playerDetected)
        {
            SwitchState(States.Attack);
        }
        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
        }
    }
    private void ExitMovingState()
    {

    }

    //---------Knockback----------------------------
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        rb.velocity = movement;
        animator.SetBool("Knockback", true);
    }
    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(States.Moving);
        }
    }
    private void ExitKnockbackState()
    {
        animator.SetBool("Knockback", false);
    }

    //---------Attack----------------------------
    private void EnterAttackState()
    {
        animator.SetBool("isAttacking", true);
        attackingStartTime = Time.time;
        Instantiate(spit, attackTrigger.position, attackTrigger.rotation);
    }
    private void UpdateAttackState()
    {
        if (Time.time >= attackingStartTime + attackingDuration)
        {
            SwitchState(States.Moving);
        }
    }
    private void ExitAttackState()
    {
        animator.SetBool("isAttacking", false);
    }

    //---------Death----------------------------
    private void EnterDeathState()
    {
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(3f, .1f);
        FindObjectOfType<AudioManager>().Play("ScorpionDies");
        Destroy(this.gameObject, 0.0f);
    }

    //--------- Other Functions ----------------------------
    public void Damage(float[] attackDetails)
    {
        // Decrease the health
        currentHealth -= attackDetails[0];

        // Check position of player
        if (attackDetails[1] > transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        // Change color
        if (bodyParts.Length > 0)
        {
            StartCoroutine(flash());
        }


        if (currentHealth > 0.0f)
        {
            SwitchState(States.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(States.Death);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void SwitchState(States state)
    {
        switch (currentState)
        {
            case States.Moving:
                ExitMovingState();
                break;
            case States.Knockback:
                ExitKnockbackState();
                break;
            case States.Attack:
                ExitAttackState();
                break;
        }

        switch (state)
        {
            case States.Moving:
                EnterMovingState();
                break;
            case States.Knockback:
                EnterKnockbackState();
                break;
            case States.Attack:
                EnterAttackState();
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
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallcheck.position, new Vector2(wallcheck.position.x + wallCheckDistance, wallcheck.position.y));
        Gizmos.DrawLine(playerCheck.position, new Vector2(playerCheck.position.x + playerCheckDistance, playerCheck.position.y));
    }
}
