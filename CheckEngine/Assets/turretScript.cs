using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    private float tba;
    public float stba = 1;
    public float playerRange = 10f;

    private Transform playerPos;
    private Transform fpPos;
    private Rigidbody2D rb;
    private Rigidbody2D rbFirePoint;
    public GameObject firePoint;
    public GameObject turretBullet;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        rbFirePoint = firePoint.GetComponent<Rigidbody2D>();
        fpPos = firePoint.GetComponent<Transform>();
    }

    void Update()
    {
        var dist = Vector2.Distance(playerPos.position, this.transform.position);
        Vector3 direction = playerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (dist <= playerRange)
        {
            rbFirePoint.rotation = angle;
            rb.rotation = angle;


            if (tba <= 0)
            {
                Invoke("Shoot", 0.1f);
                tba = stba;
            }
            else
            {
                tba -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        Instantiate(turretBullet, fpPos.position, fpPos.rotation);
    }
}