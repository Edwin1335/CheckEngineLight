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
    private PlayerMovement pm;
    float speed;
    private bool respawning = false;
    private float currentSpawnPosition;

    void Start()
    {
        currentSpawnPosition = 0;
        pm = this.GetComponent<PlayerMovement>();
        speed = pm._moveSpeed;
        playerSpawn = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    void Update()
    {
        pm = this.GetComponent<PlayerMovement>();
        HealthSystem hs = this.GetComponent<HealthSystem>();

        if (hs.health == 0 && !respawning)
        {
            pm._moveSpeed = 0;
            top.gameObject.SetActive(false);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            hs.health += 1;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("GloomyDeath");
            StartCoroutine(Respawn());
        }

    }

    IEnumerator Respawn()
    {
        respawning = true;
        yield return new WaitForSeconds(2f);
        pm._moveSpeed = speed;
        top.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        this.transform.position = playerSpawn;
        respawning = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "HP")
        {
            // If we find  adifferent/new campfire. Play sound.
            if (currentSpawnPosition != coll.transform.position.x)
            {
                FindObjectOfType<AudioManager>().Play("Campfire");
            }
            playerSpawn = new Vector2(coll.transform.position.x, coll.transform.position.y);
            currentSpawnPosition = coll.transform.position.x;
        }
    }

}
