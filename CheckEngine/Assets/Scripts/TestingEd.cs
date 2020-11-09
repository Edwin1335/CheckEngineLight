using UnityEngine;

public class TestingEd : MonoBehaviour
{
    // SerializeField shows the data in Unity and can be modified.  
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 0;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool extraJump;

    // Private variables that will not be changes in the unity window. 
    private float checkRadius = 0.1f;
    private float moveInputHorizontal;
    private bool facingRight;
    private bool isGrounded;
    private bool playerIsFalling;
    private int extraJumps = 1;

    // Get Glommys different components needed to jump/run/dash, etc.
    private Transform groundCheck;
    private Rigidbody2D rb;
    private Animator animator;

    // Start is used to get components at the start of the scene.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = this.gameObject.transform.GetChild(3).transform;
    }

    //Update is used to check input
    private void Update()
    {
        // Check if player presses the <- or -> buttons.
        moveInputHorizontal = Input.GetAxisRaw("Horizontal");

        // Check if player is on touching the ground.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Check if player presses the jump button "Space bar";
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerJump();
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
        if (isGrounded)
        {
            Debug.Log("Goes Here");
            rb.velocity = Vector2.up * jumpForce;
            extraJumps = 2;
            animator.SetTrigger("takeOff");
            animator.SetBool("isJumping", true);
        }
        else if (!isGrounded && extraJumps > 0 && extraJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetTrigger("takeOff");
            animator.SetBool("isJumping", true);

            // One last animation for the last jump
            if (extraJumps == 0)
            {
                Debug.Log("LAST Jump");
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
    }
}
