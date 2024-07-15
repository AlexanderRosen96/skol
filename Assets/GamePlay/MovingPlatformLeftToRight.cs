using UnityEngine;

public class MovingPlatformLeftToRight : MonoBehaviour, IPlatformEffect
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    private Vector2 startPosition;

    public void ApplyEffect(GameObject platform)
    {
        Debug.Log("Applying MovingPlatformLeftToRight effect.");
        startPosition = platform.transform.position;
    }

    void Update()
    {
        if (startPosition != null)
        {
            Vector2 newPosition = startPosition;
            newPosition.x += Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            transform.position = newPosition;  // Använder transform.position istället för platform.transform.position
            Debug.Log("Updating position: " + transform.position);
        }
    }


}
