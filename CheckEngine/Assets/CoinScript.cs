using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        ScoreText.gemAmount += 1;
        Destroy(gameObject);
    }
}
