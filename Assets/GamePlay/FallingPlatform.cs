using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour, IPlatformEffect
{
    public float fallDelay = 1f; // Tiden innan plattformen b�rjar falla
    private Rigidbody2D rb;
    private bool playerOnPlatform;
    public float destroyYPosition = -10f; // Y-positionen d�r plattformen f�rst�rs

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // G�r plattformen kinematisk tills den faller
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
            rb.bodyType = RigidbodyType2D.Dynamic; // G�r plattformen dynamisk s� att den faller
            rb.gravityScale = 1f; // S�tter gravitationen f�r att f� plattformen att falla
        }
    }

    public void ApplyEffect(GameObject platform)
    {
        // H�r skulle du implementera logiken f�r att applicera fallande effekten p� plattformen.
        // Detta kan inneb�ra att aktivera fallande beteende eller anropa metoder som g�r plattformen
        // dynamisk och startar dess fall.
    }
}
