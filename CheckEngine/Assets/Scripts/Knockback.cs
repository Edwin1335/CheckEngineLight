using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackStrength;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();
        
        if(rb != null)
        {
            Vector2 direction =  other.transform.position - transform.position;
            direction.y = 0;

            rb.AddForce(direction.normalized * knockbackLength, ForceMode2D.Impulse);
        }
    }
}
