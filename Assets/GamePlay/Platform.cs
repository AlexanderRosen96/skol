using UnityEngine;

public class Platform : MonoBehaviour
{
    public int pointValue = 1;
    private bool isScored = false; 

    private MovingPlatformLeftToRight movingLeftToRight;
    private MovingPlatformUpAndDown movingUpDown;
    private ExpandingPlatform expanding;
    private FallingPlatform falling;
    public LayerMask platformLayer;
    private int randomEffect;

    void Awake()
    {
        movingLeftToRight = gameObject.AddComponent<MovingPlatformLeftToRight>();
        movingLeftToRight.enabled = false;

        movingUpDown = gameObject.AddComponent<MovingPlatformUpAndDown>();
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
    public void ActivateEffect(int number)
    {

        movingLeftToRight.ActivateEffect(false);
        movingUpDown.ActivateEffect(false);
        expanding.ActivateEffect(false);
        falling.ActivateEffect(false);


        if (Random.Range(0f, 1f) > 0.2f)
        {
            switch (number)
            {
                case 1:
                    movingLeftToRight.ActivateEffect(true);
                    break;

                case 2:
                    movingUpDown.ActivateEffect(true);
                    break;  

                case 3:
                    randomEffect = Random.Range(0, 2);
                    if(randomEffect ==  0)
                        movingLeftToRight.ActivateEffect(true);
                    else 
                        movingUpDown.ActivateEffect(true);
                    break;

                case 4:
                    expanding.ActivateEffect(true);
                    break;

                case 5:
                    falling.ActivateEffect(true);
                    break;

                case 6:
                    randomEffect = Random.Range(0, 2);
                    if (randomEffect == 0)
                        movingLeftToRight.ActivateEffect(true);
                    else
                        falling.ActivateEffect(true);
                    break;

                case 7:
                    randomEffect = Random.Range(0, 2);
                    if (randomEffect == 0)
                        expanding.ActivateEffect(true);
                    else
                        movingUpDown.ActivateEffect(true);
                    break;

                case 8:
                    randomEffect = Random.Range(0, 3);
                    if (randomEffect == 0)
                        falling.ActivateEffect(true);
                    else if (randomEffect == 1)
                        movingLeftToRight.ActivateEffect(true);
                    else
                        movingUpDown.ActivateEffect(true);
                    break;

                case 9:
                    randomEffect = Random.Range(0, 4);
                    if (randomEffect == 0)
                        falling.ActivateEffect(true);
                    else if (randomEffect == 1)
                        movingLeftToRight.ActivateEffect(true);
                    else if (randomEffect == 2) 
                        expanding.ActivateEffect(true);
                    else
                        movingUpDown.ActivateEffect(true);
                    break;

                default: break;
            }
        }
    }
}
