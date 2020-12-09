using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAI : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingRight = true;
    private Transform groundDetecion;
    private TakeDamage damage;
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundDetecion = this.transform.GetChild(1).transform;
        damage.enemyName = "Scorpion";
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetecion.position, Vector2.down, distance);

        //Animations.
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            animator.SetBool("isWalking", true);

        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //Edge collider   
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
