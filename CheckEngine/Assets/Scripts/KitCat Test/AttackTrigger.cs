using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        List<Collider2D> recentDamageList = new List<Collider2D>();
        recentDamageList.Clear();

        if (col.tag == "Enemy")
        {
            if (!recentDamageList.Contains(col))
            {
                recentDamageList.Add(col);
                col.SendMessageUpwards("EnemyDamage", 1);
                Debug.Log("Attacked" + col.name);
            }
        }
    }
}
