using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    [SerializeField] private Vector2 knockbackSpeed;
    [SerializeField] private SpriteRenderer[] bodyParts;
    [SerializeField] private Color hurtColor;
    [SerializeField] private Color originalColor;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Animator animator;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;
    private PlayerAttack playerAttack;
    private PlayerJump playerJump;
    private PlayerWallJump playerWallJump;
    private PlayerGroundPound playerGroundPound;
    private PlayerPaintBomb playerPaintBomb;
    private deathSystem death;

    private Rigidbody2D rb;
    private bool playerGotAttacked = false;
    private int damageDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();
        playerAttack = GetComponent<PlayerAttack>();
        playerJump = GetComponent<PlayerJump>();
        playerWallJump = GetComponent<PlayerWallJump>();
        playerGroundPound = GetComponent<PlayerGroundPound>();
        playerPaintBomb = GetComponent<PlayerPaintBomb>();
        death = GetComponent<deathSystem>();
    }

    private void Start()
    {
        if (bodyParts.Length > 0)
        {
            originalColor = bodyParts[0].GetComponent<SpriteRenderer>().color;
        }
    }

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerGotAttacked)
        {
            StartCoroutine(waiter());
        }
        else
        {
            playerController.enabled = true;
            playerMovement.enabled = true;
            playerDash.enabled = true;
            playerAttack.enabled = true;
            playerJump.enabled = true;
            playerWallJump.enabled = true;
            playerGroundPound.enabled = true;
            playerPaintBomb.enabled = true;
            death.enabled = true;
            animator.SetBool("Knockback", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check position of player
        if (collision.transform.position.x > transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Projectile")
        {
            Damage(1);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(.3f);
        playerGotAttacked = false;
    }


    void Damage(int a)
    {
        health = health - a;
        playerController.enabled = false;
        rb.velocity = new Vector2(0, 0);
        playerController.enabled = false;
        playerMovement.enabled = false;
        playerDash.enabled = false;
        playerAttack.enabled = false;
        playerJump.enabled = false;
        playerWallJump.enabled = false;
        playerGroundPound.enabled = false;
        playerPaintBomb.enabled = false;
        death.enabled = false;
        rb.AddForce(new Vector2(knockbackSpeed.x * damageDirection, knockbackSpeed.y));
        playerGotAttacked = true;
        animator.SetBool("Knockback", true);
        // Change color
        if (bodyParts.Length > 0)
        {
            StartCoroutine(flash());
        }
    }

    IEnumerator flash()
    {

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = originalColor;
        }
    }
}
