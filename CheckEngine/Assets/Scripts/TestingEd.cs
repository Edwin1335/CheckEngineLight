using System.Collections;
using UnityEngine;

public class TestingEd : MonoBehaviour
{
    public static TestingEd instance;

    // SerializeField shows the data in Unity and can be modified. 
    [Header("Movement")]
    [SerializeField] private float speed;
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 0;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private bool extraJump;
    [SerializeField] private float holdJumpTimer = 2;

    // Private variables that will not be changes in the unity window. 
    private float checkRadius = 1f;
    private float moveInputHorizontal;
    private bool facingRight;
    private bool isGrounded;
    private bool hitRoof;
    private bool hittingRoof;
    private bool playerIsFalling;
    private int extraJumps = 1;
    private float jumpTimeCounter;
    private bool isJumping;

    // Get Glommys different components needed to jump/run/dash, etc.
    private Transform groundCheck;
    private Transform roofCheck;
    private Rigidbody2D rb;
    private Animator animator;

    // Start is used to get components at the start of the scene.
    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = this.gameObject.transform.GetChild(3).transform;
        roofCheck = this.gameObject.transform.GetChild(4).transform;
    }

    //Update is used to check input
    private void Update()
    {
        // Check if player presses the <- or -> buttons.
        moveInputHorizontal = Input.GetAxisRaw("Horizontal");

        // Check if player is on touching the ground.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            hittingRoof = false;
        }

        // Check if player has hit a roof.
        hitRoof = Physics2D.OverlapCircle(roofCheck.position, checkRadius, whatIsGround);
        if (hitRoof)
        {
            hittingRoof = true;
        }

        // Check if player presses the jump/holds "Space bar" button ;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerJump();
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpTimeCounter = 0;
            isJumping = false;
        }
        if (isJumping)
        {
            Debug.Log("Jumping");
        }
        else
        {
            Debug.Log("Not Jumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            // 
        }
    }

    // Fixed updates is used to preform operations and animations on input.
    private void FixedUpdate()
    {
        // Funtion to move the player left and right and animate him.
        movePlayer(moveInputHorizontal);
        checkAnimationStatus();

        if (isGrounded)
        {
            Debug.Log("IS GROUNDED");
        }
        else
        {
            Debug.Log("Not Grounded");
        }
    }

    // private funtion to move the position based on horizontal input.
    private void movePlayer(float horizInput)
    {
        // Display horizontal input
        Debug.Log("Horsizontal input " + moveInputHorizontal);

        // Move the player 
        rb.velocity = new Vector2(moveInputHorizontal * speed, rb.velocity.y);

        // Perform animations if moveing
        if (horizInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Flip player left or right based on direction hes looking at.
        if (facingRight == false && moveInputHorizontal < 0)
        {
            flipPLayer();
        }
        else if (facingRight == true && moveInputHorizontal > 0)
        {
            flipPLayer();
        }

    }

    // Funcion to flip the player if facing right/left.
    private void flipPLayer()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Funtion to make/animate the player jump.
    private void playerJump()
    {
        if (isGrounded && !hittingRoof)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps = maxJumps;
            animator.SetTrigger("takeOff");
            animator.SetBool("isJumping", true);
            jumpTimeCounter = holdJumpTimer;
            isJumping = true;
        }
        else if (!isGrounded && extraJumps > 0 && extraJump && !hittingRoof)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetTrigger("takeOff");
            animator.SetBool("isJumping", true);
            jumpTimeCounter = holdJumpTimer;
            isJumping = true;

            // One last animation for the last jump
            if (extraJumps == 0)
            {
                animator.SetTrigger("lastJump");
                animator.SetBool("isJumping", false);
                animator.ResetTrigger("lastJump");
            }
        }
    }

    private void checkAnimationStatus()
    {
        // If falling
        if (!isGrounded && rb.velocity.y < -0.1)
        {
            // If the velocity is negative and its not touching the ground. Play falling animation.
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
            animator.ResetTrigger("takeOff");
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if (isGrounded && animator.GetCurrentAnimatorStateInfo(0).IsName("Gloomy_Jump"))
        {
            animator.SetBool("isJumping", false);
            animator.SetTrigger("cancelTakeOff");
        }
        if (isGrounded && animator.GetCurrentAnimatorStateInfo(0).IsName("Gloomy_Take_Off"))
        {
            animator.SetTrigger("cancelTakeOff");
        }
    }

    public void Knockback(float knockBackDuration, float knockBackPower, Transform obj)
    {

        Vector2 direction = (obj.transform.position - this.transform.position).normalized;
        rb.AddForce(-obj.transform.position * knockBackPower);
    }
}
