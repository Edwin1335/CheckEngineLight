using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabeeAIScript : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    private bool playerFound = false;

    // Get Components
    private Animator animator;
    private Transform player;
    void Start()
    {
        // Check if player exists in the scene.
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerFound = true;
            animator = GetComponent<Animator>();
        }
    }


    void Update()
    {
        if (playerFound)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
            {
                animator.SetBool("isAttacking", false);
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= attackRange)
            {
                // Attack Animation.
                animator.SetBool("isAttacking", true);
                //reduce player health
                //attack player once every x amount of secs
            }

            // Flip tabbe to face player.
            if (this.transform.position.x < player.position.x)
            {
                this.transform.localScale = new Vector2(1, 1);
            }
            else if (this.transform.position.x > player.position.x)
            {
                this.transform.localScale = new Vector2(-1, 1);
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
