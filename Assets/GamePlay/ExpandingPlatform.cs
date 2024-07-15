using System.Collections;
using UnityEngine;

public class ExpandingPlatform : MonoBehaviour, IPlatformEffect
{
    public float expandSpeed = 2f; // Hastigheten som plattformen expanderar med
    public float maxScale = 2f; // Maximal skala för expansion
    public float minScale = 0.5f; // Minimal skala för kontraktion
    private Vector3 initialScale; // Initiala skalan för plattformen

    void Start()
    {
        initialScale = transform.localScale; // Spara den initiala skalan
    }

    void Update()
    {
        // Beräkna ny skala
        float scaleFactor = (Mathf.Sin(Time.time * expandSpeed) + 1) / 2; // Normalisera sin-värdet mellan 0 och 1
        float newScaleX = Mathf.Lerp(minScale, maxScale, scaleFactor);
        transform.localScale = new Vector3(newScaleX, initialScale.y, initialScale.z);
    }

    public void ApplyEffect(GameObject platform)
    {
        // Du behöver implementera ApplyEffect från IPlatformEffect-gränssnittet här
        // Eftersom plattformen redan expanderar i Update-metoden, kan du antingen lägga till
        // en metod som anropar expansionen, eller så kan du skriva din ApplyEffect-metod här
        // för att anpassa expansionen för den specifika plattformen.
    }
}
