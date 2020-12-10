using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpit : MonoBehaviour
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
        HealthSystem hs;
        bool healthSysyFound = false;

        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        if (hs = thePlayer.GetComponent<HealthSystem>())
        {
            healthSysyFound = true;
        }

        if (coll.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Splash");
            DestroyProj();
            if (healthSysyFound)
            {
                hs.health -= 1;
            }
        }
        else if (coll.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroyProj();
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

}
