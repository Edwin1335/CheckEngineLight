using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabeeAIScript : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= attackRange)
        {
            //attack anim
            //reduce player health
            //attack player once every x amount of secs
        }

        if (this.transform.position.x < player.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else if (this.transform.position.x > player.position.x)
        {
            this.transform.localScale = new Vector2(-1, 1);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
