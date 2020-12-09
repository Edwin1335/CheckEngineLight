using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        float[] attackDetails = new float[2];
        attackDetails[0] = 1;
        attackDetails[1] = this.GetComponentInParent<Transform>().transform.position.x;
        // Check if the collider hit an enemy.
        if (col.tag == "Enemy")
        {
            FindObjectOfType<AudioManager>().Play("GloomyHit");
            col.SendMessageUpwards("Damage", attackDetails);
        }
    }
}
