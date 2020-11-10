using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabeeAIScript : MonoBehaviour
{
    // Modifiable varables 
    public float speed;
    public float lineOfSight;
    public float attackRange;

    // Hiiden varaibles
    private bool playerFound = false;
    private bool flyTowardsPlayer = false;
    private bool attacked = false;

    // Get Components
    private Animator animator;
    private Transform player;
    private Vector2 originalPosition;

    void Start()
    {
        // Check if player exists in the scene.
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerFound = true;
            animator = GetComponent<Animator>();
            originalPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }


    void Update()
    {
        if (playerFound && !attacked)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
            {
                flyTowardsPlayer = true;
                animator.SetBool("isAttacking", false);
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distanceFromPlayer > lineOfSight && distanceFromPlayer > attackRange)
            {
                // Move tabbee back to where he was.
                flyTowardsPlayer = false;
                transform.position = Vector2.MoveTowards(this.transform.position, originalPosition, (speed / 2) * Time.deltaTime);
            }
            else if (distanceFromPlayer <= attackRange)
            {
                // Attack Animation.
                animator.SetBool("isAttacking", true);

                // Must still move towards the player so that it could collide and the player can take damage on collision.
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                //reduce player health
                //attack player once every x amount of secs
            }

            // Flip Tabee to face player or previous position.
            if (flyTowardsPlayer)
            {
                if (this.transform.position.x < player.position.x)
                {
                    this.transform.localScale = new Vector2(1, 1);
                }
                else if (this.transform.position.x > player.position.x)
                {
                    this.transform.localScale = new Vector2(-1, 1);
                }
            }
            else
            {
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //TestingEd.instance.Knockback(1f, 100f, this.transform);
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        attacked = true;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(3f);
        attacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
