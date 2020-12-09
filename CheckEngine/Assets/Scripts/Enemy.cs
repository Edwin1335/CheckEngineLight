using System.Collections;
using UnityEngine;

public class TestingEnemy : MonoBehaviour
{
    // Different stets the enemy can be in.
    private States currentState;
    enum States
    {
        Moving,
        Knockback,
        Death
    }

    [SerializeField] private float groundCheckDistance, wallCheckDistance, movSpeed;
    [SerializeField] private float maxHealth, knockbackDuration;
    [SerializeField] private Transform groundCheck, wallcheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 knockbackSpeed;
    [Header("Attacked")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField]

    private Color originalColor;
    private bool groundDetected, walldetected;
    private int facingDirection;
    private int damageDirection;
    private int facingRight;

    private float currentHealth;
    private float knockbackStartTime;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        facingDirection = 1;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            case States.Death:
                UpdateDeathState();
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
        walldetected = Physics2D.Raycast(wallcheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || walldetected)
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
    private void ExitKnockbackSate()
    {
        animator.SetBool("Knockback", false);
    }

    //---------Death----------------------------
    private void EnterDeathState()
    {
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(8f, .2f);
        FindObjectOfType<AudioManager>().Play("ScorpionDies");
        Destroy(this.gameObject, 0.0f);
    }
    private void UpdateDeathState()
    {

    }
    private void ExitDeathState()
    {

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
        this.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(States state)
    {
        switch (currentState)
        {
            case States.Moving:
                ExitMovingState();
                break;
            case States.Knockback:
                ExitKnockbackSate();
                break;
            case States.Death:
                ExitDeathState();
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
    }
}
