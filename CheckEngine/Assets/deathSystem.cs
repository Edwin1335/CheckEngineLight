using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSystem : MonoBehaviour
{
    public GameObject deathEffect;
    Vector2 playerSpawn;
    public GameObject top;
    public GameObject left;
    public GameObject right;
    float speed;

    void Start()
    {
        PlayerMovement pm = this.GetComponent<PlayerMovement>();
        speed = pm._moveSpeed;
        playerSpawn = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    void Update()
    {
        PlayerMovement pm = this.GetComponent<PlayerMovement>();
        HealthSystem hs = this.GetComponent<HealthSystem>();

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(2f);
            pm._moveSpeed = speed;
            top.gameObject.SetActive(true);
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
            this.transform.position = playerSpawn;
        }

        if (hs.health == 0)
        {
            pm._moveSpeed = 0;
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            hs.health += 1;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(Respawn());
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "HP")
        {
            playerSpawn = new Vector2(coll.transform.position.x, coll.transform.position.y);
        }
    }

}
