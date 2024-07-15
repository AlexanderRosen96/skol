using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour, IPlatformEffect
{
    public float fallDelay = 1f; // Tiden innan plattformen börjar falla
    private Rigidbody2D rb;
    private bool playerOnPlatform;
    public float destroyYPosition = -10f; // Y-positionen där plattformen förstörs

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Gör plattformen kinematisk tills den faller
    }

    void Update()
    {
        // Kolla om plattformen har fallit under en viss Y-position
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            StartCoroutine(FallAfterDelay());
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
        yield return new WaitForSeconds(fallDelay);
        if (playerOnPlatform)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Gör plattformen dynamisk så att den faller
            rb.gravityScale = 1f; // Sätter gravitationen för att få plattformen att falla
        }
    }

    public void ApplyEffect(GameObject platform)
    {
        // Här skulle du implementera logiken för att applicera fallande effekten på plattformen.
        // Detta kan innebära att aktivera fallande beteende eller anropa metoder som gör plattformen
        // dynamisk och startar dess fall.
    }
}
