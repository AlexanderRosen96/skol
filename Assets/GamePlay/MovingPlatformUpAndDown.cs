using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IPlatformEffect
{
    public float moveSpeed = 2f; // Hastigheten plattformen rör sig med
    public float moveDistance = 3f; // Avståndet plattformen rör sig upp och ner
    private Vector2 startPosition; // Startpositionen för plattformen

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Beräkna ny position
        Vector2 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = newPosition;
    }

    public void ApplyEffect(GameObject platform)
    {
        Update();
    }
}
