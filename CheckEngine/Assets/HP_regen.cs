using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_regen : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject thePlayer = GameObject.Find("Gloomy");
        HealthSystem SN = thePlayer.GetComponent<HealthSystem>();

        if (col.CompareTag("Player") && SN.health != 5)
        {
            SN.health = 5;
        }
    }
}
