using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBoss : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private void Awake(){
        _rigidBody.GetComponent<Rigidbody2D>();
        _collider.GetComponent<Collider2D>();
    }

    void Update()
    {
        // if (playerFound && !attacked)
        // {
        //     float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        //     if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        //     {
        //         flyTowardsPlayer = true;
        //         animator.SetBool("isAttacking", false);
        //         transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        //     }
        //     else if (distanceFromPlayer > lineOfSight && distanceFromPlayer > attackRange)
        //     {
        //         // Move tabbee back to where he was.
        //         flyTowardsPlayer = false;
        //         transform.position = Vector2.MoveTowards(this.transform.position, originalPosition, (speed / 2) * Time.deltaTime);
        //     }
        //     else if (distanceFromPlayer <= attackRange)
        //     {
        //         // Attack Animation.
        //         animator.SetBool("isAttacking", true);

        //         // Must still move towards the player so that it could collide and the player can take damage on collision.
        //         transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        //         //reduce player health
        //         //attack player once every x amount of secs
        //     }

        //     // Flip Tabee to face player or previous position.
        //     if (flyTowardsPlayer)
        //     {
        //         if (this.transform.position.x < player.position.x)
        //         {
        //             this.transform.localScale = new Vector2(1, 1);
        //         }
        //         else if (this.transform.position.x > player.position.x)
        //         {
        //             this.transform.localScale = new Vector2(-1, 1);
        //         }
        //     }
        //     else
        //     {
        //         if (this.transform.position.x < originalPosition.x)
        //         {
        //             this.transform.localScale = new Vector2(1, 1);
        //         }
        //         else if (this.transform.position.x > originalPosition.x)
        //         {
        //             this.transform.localScale = new Vector2(-1, 1);
        //         }

        //     }
        // }
    }
}
