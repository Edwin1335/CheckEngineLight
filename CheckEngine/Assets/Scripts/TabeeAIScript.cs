using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabeeAIScript : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    private bool playerFound = false;
    private bool flyTowardsPlayer = false;

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
        if (playerFound)
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
                //reduce player health
                //attack player once every x amount of secs
            }

            // Flip Tabee to face player.
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
