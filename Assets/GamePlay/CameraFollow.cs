using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referens till spelarens transform
    public float smoothSpeed = 0.125f; // Hastighet på kamerans rörelse
    public Vector3 offset; // Offset för att justera kamerans position relativt till spelaren

    private float initialY; // För att spara initiala y-positionen av kameran

    void Start()
    {
        initialY = transform.position.y; // Spara den initiala y-positionen
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        // Behåll den initiala y-positionen om spelaren är under den initiala y-positionen
        if (player.position.y < initialY)
        {
            desiredPosition.y = initialY;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
