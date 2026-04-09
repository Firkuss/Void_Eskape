using System.Collections;
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

    public Vector2 endPos = new Vector2(3, 2);

    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isGrounded;
    private float moveInput;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        rb.freezeRotation = true;
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

        if (collision.gameObject.tag == "Jumper")
        {
            print("I jump");
            coll.enabled = false;
            Wait();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + 2, 4);
            transform.localScale = new Vector2(0.7f, 0.7f); 
            PlaneChanging();
        }
    }

    void isDead() 
    {
        Destroy(gameObject);
    }

    void PlaneChanging()
    {
        GameObject[] AllPlane2 = GameObject.FindGameObjectsWithTag("Plane2");
        GameObject[] AllPlane1 = GameObject.FindGameObjectsWithTag("Plane1");
        foreach (GameObject P2 in AllPlane2)
        {
            P2.GetComponent<Collider2D>().isTrigger = false;
        }
        foreach (GameObject P1 in AllPlane1)
        {
            P1.GetComponent<Collider2D>().isTrigger = true;
        }
            
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        coll.enabled = true;
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
