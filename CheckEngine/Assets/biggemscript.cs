using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biggemscript : MonoBehaviour
{
    public GameObject gameover;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ScoreText.gemAmount += 100;
            FindObjectOfType<AudioManager>().Play("Collect");
            Destroy(gameObject);
            gameover.SetActive(true);
        }


    }
}
