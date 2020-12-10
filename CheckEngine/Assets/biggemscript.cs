using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class biggemscript : MonoBehaviour
{
    public GameObject gameover;
    private bool loadNew = false;
    private float time;

    private void Update() 
    {
        if(loadNew)
        {
            if(Time.time >= time + 20.0f)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ScoreText.gemAmount += 100;
            FindObjectOfType<AudioManager>().Play("Collect");
            Destroy(gameObject);
            gameover.SetActive(true);
            loadNew = true;
            time = Time.time;
            SceneManager.LoadScene(2);
        }
    }
}
