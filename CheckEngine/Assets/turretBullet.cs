using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public GameObject impactEffect;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        HealthSystem hs = thePlayer.GetComponent<HealthSystem>();

        if (coll.CompareTag("Player"))
        {
            DestroyProj();
            hs.health -= 1;
        }
        else
        {
            return;
        }
    }

    void DestroyProj()
    {
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}