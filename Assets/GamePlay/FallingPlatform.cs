using System.Collections;
using UnityEngine;

public class FallingPlatform : DynamicEffectController
{
    private float fallDelay = 2f;
    private bool playerOnPlatform = false;
    private bool platformReadyToFall = false;
    private int platformsLife = 1;
    private float destroyYPosition = -9f; 


    void Start()
    {
        toStart();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is not attached to the GameObject.");
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            if (normal.y < -0.5f)
            {
                playerOnPlatform = true;
                StartCoroutine(FallAfterDelay());
            }
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("PlatformExpanding"))
        {
            if(platformsLife == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           playerOnPlatform = false;
        }
    }

    IEnumerator FallAfterDelay()
    {
        if (!platformReadyToFall)
        {
            yield return new WaitForSeconds(fallDelay);
            platformReadyToFall=true;
        }
       
        else
        {
            if (playerOnPlatform)
            {
                platformsLife = 0;
                yield return new WaitForSeconds(fallDelay-1.7f);
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1f;
            }
        }

    }
    public void ActivateEffect(bool activate)
    {
        enabled = activate;
    }
}
