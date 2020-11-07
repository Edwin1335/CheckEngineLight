using UnityEngine;

public class TestingEd : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 0;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float moveInputHorizontal;
    private bool facingRight;
    private bool isGrounded;
    private bool pressedJumpButton;
    private bool playerIsFalling;

    private Rigidbody2D rb;
    private Animator animator;

    // Start is used to get components at the start of the scene.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            Debug.Log("IS GROUNDED");
        }
        // Check if player presses the UP button.
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            pressedJumpButton = true;
        }
    }

    // Fixed updates is used to preform operations and animations on input.
    private void FixedUpdate()
    {
        // Funtion to move the player left and right and animate him.
        movePlayer(moveInputHorizontal);

        // Funtion to make the player Jump.
        if (pressedJumpButton)
        {
            playerJump();
        }

        if (pressedJumpButton && isGrounded)
        {
            pressedJumpButton = false;
            animator.SetTrigger("takeOff");
            animator.SetBool("isJumping", true);
        }

        // If the player is falling, play animation.
        if (!isGrounded && rb.velocity.y < -0.1)
        {
            playerIsFalling = true;
            animator.SetBool("isFalling", true);
        }

        // If falling and landed, play landin animation
        if (playerIsFalling && isGrounded)
        {
            playerIsFalling = false;
            animator.SetBool("isFalling", false);
        }

        if(!playerIsFalling && isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

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

    private void flipPLayer()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void playerJump()
    {
        Debug.Log("Goes Here");
        rb.velocity = Vector2.up * jumpForce;
    }
}
