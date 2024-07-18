using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; // Singleton instans
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpDelay = 0.5f;
    private bool isGrounded;
    private bool justJumped;
    private bool isJumping;
    private Rigidbody2D rb;
    private Collider2D coll;
    private float jumpTimer;
    private float points;

    void Awake()
    {
        Instance = this; // Tilldela singleton-instansen
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        jumpTimer = 0f;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (isGrounded && !justJumped && jumpTimer <= 0)
        {
            justJumped = true;
            jumpTimer = jumpDelay;
            StartCoroutine(JumpAfterDelay());
        }
    }

    IEnumerator JumpAfterDelay()
    {
        isJumping = true;
        yield return new WaitForSeconds(jumpDelay);
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
        isJumping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("PlatformExpanding"))
        {
            isGrounded = false;
            transform.SetParent(null);
        }
    }

    void HandleCollision(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("PlatformExpanding"))
            {
                Vector2 normal = contact.normal;
                if (normal.y > 0.5f)
                {
                    if (!collision.gameObject.CompareTag("PlatformExpanding"))
                    {
                        isGrounded = true;
                        justJumped = false;
                        transform.SetParent(collision.transform);
                        Platform platform = collision.gameObject.GetComponent<Platform>();
                        if (platform != null && platform.GiveScore())
                        {
                            GameManager.Instance.AddScore(platform.pointValue);
                            points++;
                        }
                    }
                    else
                    {
                        isGrounded = true;
                        justJumped = false;
                        Platform platform = collision.gameObject.GetComponent<Platform>();
                        if (platform != null && platform.GiveScore())
                        {
                            GameManager.Instance.AddScore(platform.pointValue);
                            points++;
                        }
                    }
                }
            }
        }
    }

    public float GetPoints() { return points; }
}
