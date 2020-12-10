using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ScoreText.gemAmount += 1;
            FindObjectOfType<AudioManager>().Play("Collect");
            Destroy(gameObject);
        }
    }
}
