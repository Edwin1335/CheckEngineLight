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

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHearts)
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

        if (collision.gameObject.tag == "Tabee")
        {
            Damage(1);
            Debug.Log("Taking Damage");
        }
        else if(collision.gameObject.tag == "Rocky")
        {
            Damage(2);
            Debug.Log("Taking Damage");
        }
        // GetComponent<BoxCollider2D>().enabled = false;
        //StartCoroutine(waiter());
        //GetComponent<BoxCollider2D>().enabled = true;
    }
    IEnumerator waiter()
    {
        //Wait for 2 seconds
        yield return new WaitForSeconds(2);
    }
    void Damage(int a)
    {
        health = health - a;

    }
}
