using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject platform;
    public Animator anim;
    public GameObject pressE;

    void OnTriggerStay2D(Collider2D collision)
    {
        lever_platform lp = platform.GetComponent<lever_platform>();

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e") && ScoreText.gemAmount >= 50)
            {
                lp.on = true;
                anim.enabled = !anim.enabled;
            }
            else if(Input.GetKeyDown("e") && lp.on == true)
            {
                lp.on = false;
            }
        }

    }

}
