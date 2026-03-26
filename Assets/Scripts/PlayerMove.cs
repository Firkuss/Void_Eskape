using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Horizontal movement speed.")]
    [Range(1f, 20f)]
    public float moveSpeed = 8f;

    [Tooltip("Jump force applied when jumping.")]
    [Range(1f, 20f)]
    public float jumpForce = 12f;

    [Header("Ground Check Settings")]
    [Tooltip("Transform used to check if the player is on the ground.")]
    public Transform groundCheck;

    [Tooltip("Radius of the ground check circle.")]
    public float groundCheckRadius = 0.2f;

    [Tooltip("Layers considered as ground.")]
    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevent unwanted rotation
    }

    void Update()
    {
        // Get horizontal input (-1, 0, 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obstacle") 
        {
            isDead();
        }
    }

    void isDead() 
    {
        Destroy(gameObject);
    }

    // Draw ground check radius in Scene view for debugging
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
