using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IPlatformEffect
{
    public float moveSpeed = 2f; // Hastigheten plattformen r�r sig med
    public float moveDistance = 3f; // Avst�ndet plattformen r�r sig upp och ner
    private Vector2 startPosition; // Startpositionen f�r plattformen

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Ber�kna ny position
        Vector2 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = newPosition;
    }

    public void ApplyEffect(GameObject platform)
    {
        Update();
    }
}
