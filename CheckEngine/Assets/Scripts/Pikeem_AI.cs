using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeem_AI : MonoBehaviour
{
    [SerializeField] float pikeemMovespeed = 1f;

    Rigidbody2D pkrigbody;

    void Start()
    {
        pkrigbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingRight())
        {
            pkrigbody.velocity = new Vector2(pikeemMovespeed, 0f);
        }
        else
        {
            pkrigbody.velocity = new Vector2(-pikeemMovespeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(pkrigbody.velocity.x)), transform.localScale.y);
    }
}
