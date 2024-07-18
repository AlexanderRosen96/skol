using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public PlayerController myPlayerController;
    public GameObject platFormType1;
    public GameObject platFormType2;
    public GameObject platFormType3;
    public GameObject player;
    public Camera mainCamera;
    public List<GameObject> Platforms;
    public LayerMask platformLayer;
    private Transform playerStart;
    private int count = 0;
    private float lastCheckedPoints;

    private float cameraHight;
    private float cameraWidth;
    private float maxJumpDistance;
    private float maxJumpHeight;
    private float playerHeight = 1f;
    private float playerWidth = 0.75f;
    private Vector2 lastPosition;

    private float playerPoints;
    private float amountOfStartPlatforms = 10;
    private Vector2 _latestPlatform = new Vector3(0f, -8.6f);
    private List<GameObject> platformsList = new List<GameObject>();
    private List<System.Type> platformEffectsList = new List<System.Type>
    {
        typeof(MovingPlatformLeftToRight),
        typeof(MovingPlatformUpAndDown),
        typeof(ExpandingPlatform),
        typeof(FallingPlatform)
    };


    void Start()
    {
       // CalculateMaxJumpDistance(0, 3);
        playerStart = player.transform;
        cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        GeneratePlatforms();
        lastCheckedPoints = 0;

    }

    void Update()
    {
        playerPoints = myPlayerController.GetPoints();

        if (playerPoints >= 5 && playerPoints > lastCheckedPoints)
        {
            AddPlatform();
            lastCheckedPoints = playerPoints;
        }

        if (GameManager.Instance.score >= 5 && platformsList.Count > amountOfStartPlatforms)
        {
            RemoveOldestPlatform();
        }

        //CheckPlatformCollisions();
    }



    private void GeneratePlatforms()
    {
        lastPosition = playerStart.position;

        for (int i = 0; i < amountOfStartPlatforms; i++)
        {
            AddPlatform();
        }
    }



    private void AddPlatform()
    {

        // New code where we generate platforms based on the latest platforms position
        // to ensure the distance between them is not too far

        var randomPlatformPositionX = Random.Range(-10f, 10f);
        var randomPlatformPositionY = Random.Range(3f, 5.2f);

        var newPlatformPosition = new Vector2(_latestPlatform.x + randomPlatformPositionX, _latestPlatform.y + randomPlatformPositionY);
        var newPlatform = Instantiate(platFormType1, newPlatformPosition, Quaternion.identity);
        newPlatform.name = "Platform" + count;

        platformsList.Add(newPlatform);
       // AddEffect(newPlatform); // for testing
        if (playerPoints >= 25)
        {
           AddEffect(newPlatform);
        }
        _latestPlatform = newPlatformPosition;
        
        /*
         * 
      
                newPlatform.layer = LayerMask.NameToLayer("Platform");
        
        // Lägg till den osynliga väggen
        BoxCollider2D platformCollider = newPlatform.GetComponent<BoxCollider2D>();
        BoxCollider2D detectionCollider = newPlatform.AddComponent<BoxCollider2D>();
        detectionCollider.size = platformCollider.size + new Vector2(0.5f, 0.5f); // Gör den osynliga väggen något större
        detectionCollider.isTrigger = false; // Se till att den är en fysisk collider och inte en trigger

        // Lägg till InvisibleWall-skriptet på detectionCollider
        var invisibleWall = detectionCollider.gameObject.AddComponent<InvisibleWall>();

        Rigidbody2D rb = newPlatform.AddComponent<Rigidbody2D>();
        rb.isKinematic = true; // Gör Rigidbody2D kinematisk för att undvika att påverkas av fysik

        Platform platformScript = newPlatform.GetComponent<Platform>();
        platformScript.platformLayer = LayerMask.NameToLayer("Platform");

        float randomX;
        float randomY;
        int safetyCounter = 0;
        count++;

        do
        {
            randomX = UnityEngine.Random.Range(mainCamera.transform.position.x - (cameraWidth / 2), mainCamera.transform.position.x + (cameraWidth / 2));
            if (platformsList.Count == 0)
            {
                randomY = lastPosition.y + UnityEngine.Random.Range(playerHeight + 3, maxJumpDistance);
            }
            else
            {
                randomY = platformsList[platformsList.Count - 1].transform.position.y + UnityEngine.Random.Range(playerHeight + 3, maxJumpDistance);
            }
            safetyCounter++;
            if (safetyCounter > 1000)
            {
                Debug.LogError("Infinite loop detected in AddPlatform");
                break;
            }
        } while (PreventToClose(new Vector2(randomX, randomY)) || !IsDistanceSufficient(new Vector2(randomX, randomY)));

        Vector2 platformPosition = new Vector2(randomX, randomY);
        GameObject newPlatform = Instantiate(platFormType1, platformPosition, Quaternion.identity);
        newPlatform.name = "Platform" + count;
        platformsList.Add(newPlatform);

        if (playerPoints > 5)
        {
            AddRandomEffect(newPlatform);
        }
        */
    }

    private void RemoveOldestPlatform()
    {
        if (platformsList.Count > 0)
        {
            GameObject platformToRemove = platformsList[0];
            platformsList.RemoveAt(0);
            Destroy(platformToRemove);
        }
    }
    

    private void AddEffect(GameObject platform)
    {
        if (platformEffectsList.Count > 0)
        {
            
            Platform platformScript = platform.GetComponent<Platform>(); // Aktivera effekten på plattformen

            //System.Type effectType = platformEffectsList[randomIndex];
            //int randomIndex = Random.Range(0, platformEffectsList.Count);


            if (platformScript != null)
            {
                if (playerPoints > 25 && playerPoints <= 50 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(1);
                }
                else if (playerPoints > 50 && playerPoints <= 75 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(2);
                }
                else if (playerPoints > 75 && playerPoints <= 100 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(3);
                }
                else if (playerPoints > 100 && playerPoints <= 125 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(4);
                }
                else if (playerPoints > 125 && playerPoints <= 150 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(5);
                }
                else if (playerPoints > 150 && playerPoints <= 175 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(6);
                }
                else if (playerPoints > 175 && playerPoints <= 200 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(7);
                }
                else if (playerPoints > 200 && playerPoints <= 225 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(8);
                }
                else if (playerPoints > 225 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(9);
                }
                
            }
            
            //For testing
            /*if (platformScript != null)
            {
                platformScript.ActivateEffect(2);
            }
            */
        }
    }



    /* 
 private bool IsDistanceSufficient(Vector2 position)
 {
     foreach (var platform in platformsList)
     {
         if (Vector2.Distance(platform.transform.position, position) < playerWidth * 2)
         {
             return false;
         }
     }
     return true;
 }

 private void AdjustPositionForEffect(GameObject platform, int effectNumber)
 {
     Vector2 position = platform.transform.position;
     int safetyCounter = 0;

     if (effectNumber == 1) // Left to right movement
     {
         position.x = UnityEngine.Random.Range(mainCamera.transform.position.x - (cameraWidth / 2) + playerWidth, mainCamera.transform.position.x + (cameraWidth / 2) - playerWidth);
     }
     else if (effectNumber == 2) // Up and down movement
     {
         position.y = Mathf.Clamp(position.y, lastPosition.y, lastPosition.y + maxJumpDistance); // Korrigera startpositionen för att undvika för höga plattformar
         position.y += UnityEngine.Random.Range(-1f, 1f) * playerHeight;
     }
     platform.transform.position = position;

     while (!IsDistanceSufficient(platform.transform.position))
     {
         if (safetyCounter > 1000)
         {
             Debug.LogError("Infinite loop detected in AdjustPositionForEffect");
             break;
         }

         if (effectNumber == 1) // Left to right movement
         {
             position.x = UnityEngine.Random.Range(mainCamera.transform.position.x - (cameraWidth / 2) + playerWidth, mainCamera.transform.position.x + (cameraWidth / 2) - playerWidth);
         }
         else if (effectNumber == 2) // Up and down movement
         {
             position.y = Mathf.Clamp(position.y, lastPosition.y, lastPosition.y + maxJumpDistance); // Korrigera startpositionen för att undvika för höga plattformar
             position.y += UnityEngine.Random.Range(-1f, 1f) * playerHeight;
         }
         platform.transform.position = position;
         safetyCounter++;
     }
 }



else if (playerPoints > 15 && playerPoints < 20 && playerPoints > lastCheckedPoints)
{
  platformScript.ActivateEffect(platformEffects[2], 3);
}
else if (playerPoints > 20 && playerPoints < 25 && playerPoints > lastCheckedPoints)
{
  platformScript.ActivateEffect(platformEffects[3], 4);
}
else if (playerPoints > 25 && playerPoints > lastCheckedPoints)
{
  platformScript.ActivateEffect(effectType, 4);
}

     private void CalculateMaxJumpDistance(float startY, float endY)
 {
     float jumpForce = myPlayerController.jumpForce;
     float gravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
     float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScale);
     maxJumpHeight = (jumpForce * jumpForce) / (2f * gravity);
     maxJumpDistance = Mathf.Sqrt(maxJumpHeight * Mathf.Abs(endY - startY));
 }

     private bool PreventToClose(Vector2 newPosition)
 {
     int rightCount = 0;
     int positionsToCheck = Mathf.Min(3, platformsList.Count);

     float minDistanceX = playerWidth + 1;
     float maxDistanceX = cameraWidth / 2;
     float minDistanceY = playerHeight + 5;

     for (int i = platformsList.Count - 1; i >= platformsList.Count - positionsToCheck; i--)
     {
         Vector2 previousPosition = platformsList[i].transform.position;

         if (newPosition.x != previousPosition.x
             && (newPosition.x < previousPosition.x - minDistanceX || newPosition.x > previousPosition.x + maxDistanceX))
         {
             rightCount++;
         }
     }

     return rightCount == positionsToCheck;
 }
     private void CheckPlatformCollisions()
 {
     for (int i = 0; i < platformsList.Count; i++)
     {
         for (int j = i + 1; j < platformsList.Count; j++)
         {
             Vector2 posA = platformsList[i].transform.position;
             Vector2 posB = platformsList[j].transform.position;
             float distance = Vector2.Distance(posA, posB);

             if (distance < playerWidth * 2)
             {
                 // Flytta plattformarna isär
                 Vector2 direction = (posA - posB).normalized;
                 float overlap = playerWidth * 2 - distance;
                 platformsList[i].transform.position = posA + direction * (overlap / 2);
                 platformsList[j].transform.position = posB - direction * (overlap / 2);
             }
         }
     }
 }
*/
}
