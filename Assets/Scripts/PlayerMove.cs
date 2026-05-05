using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [Range(1f, 20f)]
    public float moveSpeed = 8f;
    [Range(1f, 20f)]
    public float jumpForce = 12f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Vector2 endPos = new Vector2(3, 2);
    public GameObject parentPlane;

    public ShopManager shopManager;

    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isGrounded;
    private float moveInput;
    private bool isPlane1;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        rb.freezeRotation = true;
        isPlane1 = true;
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

        if (isPlane1 == true)
        {
            PlaneChangingFW();
        }

        if (isPlane1 == false)
        {
            PlaneChangingBack();
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

        if (collision.gameObject.tag == "Jumper" && collision.gameObject.transform.parent.gameObject.tag == "Plane1" && isPlane1 == true)
        {
            print("I jump Back");
            coll.enabled = false;
            Wait();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + 2, 0);
            transform.localScale = new Vector2(0.7f, 0.7f);
            isPlane1 = false;
        }

        if (collision.gameObject.tag == "Jumper" && collision.gameObject.transform.parent.gameObject.tag == "Plane2" && isPlane1 == false)
        {
            print("I jump FW");
            coll.enabled = false;
            Wait();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + 2, 0);
            transform.localScale = new Vector2(1.3f, 1.3f);
            isPlane1 = true;
        }

        if (collision.gameObject.tag == "Coin" && collision.gameObject.transform.parent.gameObject.tag == "Plane1" && isPlane1 == true)
        {
            shopManager.playerCurrency += 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coin" && collision.gameObject.transform.parent.gameObject.tag == "Plane2" && isPlane1 == false)
        {
            shopManager.playerCurrency += 1;
            Destroy(collision.gameObject);
        }
    }

    void isDead() 
    {
        gameObject.SetActive(false);
        shopManager.OpenShop();
    }

    void PlaneChangingBack()
    {
        GameObject AllPlane2 = GameObject.Find("Plane2Manager");
        GameObject AllPlane1 = GameObject.Find("Plane1Manager");

        foreach (Transform P2 in AllPlane2.transform)
        {
            P2.GetComponent<Collider2D>().isTrigger = false;
            foreach (Transform gchildP2 in P2.transform)
            {
                gchildP2.GetComponent<Collider2D>().isTrigger = false;
            }
        }
        foreach (Transform P1 in AllPlane1.transform)
        {
            P1.GetComponent<Collider2D>().isTrigger = true;
            foreach (Transform gchildP1 in P1.transform)
            {
                gchildP1.GetComponent<Collider2D>().isTrigger = true;
            }
        }
            
    }

    void PlaneChangingFW()
    {
        GameObject AllPlane2 = GameObject.Find("Plane2Manager");
        GameObject AllPlane1 = GameObject.Find("Plane1Manager");

        foreach (Transform P2 in AllPlane2.transform)
        {
            P2.GetComponent<Collider2D>().isTrigger = true;
            foreach (Transform gchildP2 in P2.transform)
            {
                gchildP2.GetComponent<Collider2D>().isTrigger = true;
            }
        }
        foreach (Transform P1 in AllPlane1.transform)
        {
            P1.GetComponent<Collider2D>().isTrigger = false;
            foreach (Transform gchildP1 in P1.transform)
            {
                gchildP1.GetComponent<Collider2D>().isTrigger = false;
            }
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
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
