using UnityEngine;

public class Platform : MonoBehaviour
{
    public int pointValue = 10; // Standardpoäng för plattformen
    private bool isScored = false; // Flagga för att hålla reda på om poäng har givits

    // Referenser till effekterna
    private MovingPlatformLeftToRight movingLeftToRight;
    private MovingPlatform movingUpDown;
    private ExpandingPlatform expanding;
    private FallingPlatform falling;

    void Awake()
    {
        // Lägg till effekterna som komponenter, men håll dem inaktiva
        movingLeftToRight = gameObject.AddComponent<MovingPlatformLeftToRight>();
        movingLeftToRight.enabled = false;

        movingUpDown = gameObject.AddComponent<MovingPlatform>();
        movingUpDown.enabled = false;

        expanding = gameObject.AddComponent<ExpandingPlatform>();
        expanding.enabled = false;

        falling = gameObject.AddComponent<FallingPlatform>();
        falling.enabled = false;
    }

    // Metod för att ge poäng
    public bool GiveScore()
    {
        if (!isScored)
        {
            isScored = true;
            return true;
        }
        return false;
    }

    // Metod för att aktivera en specifik effekt
    public void ActivateEffect(System.Type effectType, int number)
    {
       // Deaktivera alla effekter först
        movingLeftToRight.enabled = false;
        movingUpDown.enabled = false;
        expanding.enabled = false;
        falling.enabled = false;

        // Aktivera den specifika effekten
        if (effectType == typeof(MovingPlatformLeftToRight)&& number == 1)
        {
            movingLeftToRight.enabled = true;
        }
        else if (effectType == typeof(MovingPlatform) && number == 2)
        {
            movingUpDown.enabled = true;
        }
        else if (effectType == typeof(ExpandingPlatform) && number == 3)
        {
            expanding.enabled = true;
        }
        else if (effectType == typeof(FallingPlatform) && number == 4)
        {
            falling.enabled = true;
        }
       
    }
}
