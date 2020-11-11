using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool takingDamage = false;

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy" && !takingDamage)
        {
            Damage(1);
            Debug.Log("Taking Damage");
            StartCoroutine(waiter());
        }
        // GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator waiter()
    {
        takingDamage = true;
        yield return new WaitForSeconds(1);
        takingDamage = false;
    }

    void Damage(int a)
    {
        health = health - a;
    }
}
