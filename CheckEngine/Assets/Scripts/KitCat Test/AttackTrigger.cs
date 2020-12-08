using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        List<Collider2D> recentDamageList = new List<Collider2D>();
        recentDamageList.Clear();

        // Check if the collider hit an enemy.
        if (col.tag == "Enemy")
        {
            // Do not hit an enemy more than once if it has more than one collider.
            if (!recentDamageList.Contains(col))
            {
                Debug.Log("Attacking " + col.name);
                recentDamageList.Add(col);
                col.GetComponent<TakeDamage>().EnemyDamage(1);
                col.GetComponent<TakeDamage>().knockBack(this.transform);
            }
        }
    }
}
