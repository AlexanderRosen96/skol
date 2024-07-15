using System.Collections;
using UnityEngine;

public class ExpandingPlatform : MonoBehaviour, IPlatformEffect
{
    public float expandSpeed = 2f; // Hastigheten som plattformen expanderar med
    public float maxScale = 2f; // Maximal skala f�r expansion
    public float minScale = 0.5f; // Minimal skala f�r kontraktion
    private Vector3 initialScale; // Initiala skalan f�r plattformen

    void Start()
    {
        initialScale = transform.localScale; // Spara den initiala skalan
    }

    void Update()
    {
        // Ber�kna ny skala
        float scaleFactor = (Mathf.Sin(Time.time * expandSpeed) + 1) / 2; // Normalisera sin-v�rdet mellan 0 och 1
        float newScaleX = Mathf.Lerp(minScale, maxScale, scaleFactor);
        transform.localScale = new Vector3(newScaleX, initialScale.y, initialScale.z);
    }

    public void ApplyEffect(GameObject platform)
    {
        // Du beh�ver implementera ApplyEffect fr�n IPlatformEffect-gr�nssnittet h�r
        // Eftersom plattformen redan expanderar i Update-metoden, kan du antingen l�gga till
        // en metod som anropar expansionen, eller s� kan du skriva din ApplyEffect-metod h�r
        // f�r att anpassa expansionen f�r den specifika plattformen.
    }
}
