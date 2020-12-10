using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabeeAI : MonoBehaviour
{
    // Different stets the enemy can be in.
    private States currentState;
    enum States
    {
        Moving,
        Knockback,
        Attack,
        Approach,
        Death
    }

    // Modifyable fields.
    [SerializeField] private int health = 4;
    [SerializeField] private float speed = 3;
    [SerializeField] private float lineOfSight = 11.0f;
    [SerializeField] private float attackRange = 1.2f;
    [Header("Attacked")]
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color originalColor;
    [SerializeField] private Vector2 knockbackSpeed = new Vector2(12.0f, 7.0f);

    // Hidden variables.
    private bool playerFound = false;
    private int damageDirection;
    public float currentHealth;
    private float knockbackStartTime;
    private float attackingStartTime;
    private float distanceFromPlayer;

    // Get componenets.
    private Animator animator;
    private Transform player;
    private Vector2 originalPosition;
    private Rigidbody2D rb;


    private void Start()
    {

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerFound = true;
            currentHealth = health;
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            originalPosition = new Vector2(this.transform.position.x, this.transform.position.y);
            FindObjectOfType<AudioManager>().Play("TabeeFly");
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
                case States.Moving:
                    UpdateMovingState();
                    break;
                case States.Knockback:
                    UpdateKnockbackState();
                    break;
                case States.Attack:
                    UpdateAttackState();
                    break;
                case States.Approach:
                    UpdateApproachState();
                    break;
            }
        }
    }

    //---------Moving----------------------------
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        // Fly towards the player.
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            SwitchState(States.Approach);
        }
        else
        {
            // Move back to original position.
            transform.position = Vector2.MoveTowards(this.transform.position, originalPosition, (speed / 2) * Time.deltaTime);
            if (this.transform.position.x < originalPosition.x)
            {
                this.transform.localScale = new Vector2(1, 1);
            }
            else if (this.transform.position.x > originalPosition.x)
            {
                this.transform.localScale = new Vector2(-1, 1);
            }
        }
    }
    private void ExitMovingState()
    {

    }

    //---------Knockback----------------------------
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
    }
    private void UpdateKnockbackState()
    {
        if (Time.time >= (knockbackStartTime + .2f))
        {
            // Fly towards the player.
            if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
            {
                SwitchState(States.Approach);
            }
            else if (distanceFromPlayer > lineOfSight && distanceFromPlayer > attackRange)
            {
                SwitchState(States.Moving);
            }
            else if (distanceFromPlayer <= attackRange)
            {
                SwitchState(States.Attack);
            }
        }
    }
    private void ExitKnockbackState()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
    }


    //---------Approach----------------------------
    private void EnterApproachState()
    {

    }
    private void UpdateApproachState()
    {
        // Approaching player
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

        if (distanceFromPlayer > lineOfSight)
        {
            SwitchState(States.Moving);
        }
        else if (distanceFromPlayer <= attackRange)
        {
            SwitchState(States.Attack);
        }

        //Flip based on players position
        if (this.transform.position.x < player.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else if (this.transform.position.x > player.position.x)
        {
            this.transform.localScale = new Vector2(-1, 1);
        }
    }
    private void ExitApproachState()
    {

    }

    //---------Attack----------------------------
    private void EnterAttackState()
    {
        animator.SetBool("isAttacking", true);

    }
    private void UpdateAttackState()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        if (distanceFromPlayer > lineOfSight)
        {
            SwitchState(States.Moving);
        }
        else if (distanceFromPlayer <= lineOfSight && distanceFromPlayer > attackRange)
        {
            SwitchState(States.Approach);
        }

        //Flip based on players position
        if (this.transform.position.x < player.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else if (this.transform.position.x > player.position.x)
        {
            this.transform.localScale = new Vector2(-1, 1);
        }
    }
    private void ExitAttackState()
    {
        animator.SetBool("isAttacking", false);
    }

    //---------Death----------------------------
    private void EnterDeathState()
    {
        // Play death Sound
        int choose = Random.Range(0, 2);
        if (choose == 0)
        {
            FindObjectOfType<AudioManager>().Play("TabeDead1");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("TabeeDead2");

        }
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        CameraShake.Instance.ShakeCamera(3f, .1f);
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
            case States.Approach:
                ExitApproachState();
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
            case States.Approach:
                EnterApproachState();
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
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
