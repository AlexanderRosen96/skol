using UnityEngine;

public class Platform : MonoBehaviour
{
    public int pointValue = 10; // Standardpo�ng f�r plattformen
    private bool isScored = false; // Flagga f�r att h�lla reda p� om po�ng har givits

    // Referenser till effekterna
    private MovingPlatformLeftToRight movingLeftToRight;
    private MovingPlatform movingUpDown;
    private ExpandingPlatform expanding;
    private FallingPlatform falling;

    void Awake()
    {
        // L�gg till effekterna som komponenter, men h�ll dem inaktiva
        movingLeftToRight = gameObject.AddComponent<MovingPlatformLeftToRight>();
        movingLeftToRight.enabled = false;

        movingUpDown = gameObject.AddComponent<MovingPlatform>();
        movingUpDown.enabled = false;

        expanding = gameObject.AddComponent<ExpandingPlatform>();
        expanding.enabled = false;

        falling = gameObject.AddComponent<FallingPlatform>();
        falling.enabled = false;
    }

    // Metod f�r att ge po�ng
    public bool GiveScore()
    {
        if (!isScored)
        {
            isScored = true;
            return true;
        }
        return false;
    }

    // Metod f�r att aktivera en specifik effekt
    public void ActivateEffect(System.Type effectType, int number)
    {
       // Deaktivera alla effekter f�rst
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
