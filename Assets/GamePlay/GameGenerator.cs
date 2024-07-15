using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    public PlayerController myPlayerController;
    public GameObject platFormType1;
    public GameObject platFormType2;
    public GameObject platFormType3;
    public GameObject player;
    public Camera mainCamera;
    public List<GameObject> Platforms;
    private Transform playerStart;
    private int count = 0;
    private float lastCheckedPoints;
    private float cameraHight;
    private float cameraWidth;
    private float maxJumpDistance;
    private float maxJumpHeight;
    private float playerHeight = 1f;
    private float playerWidth = 0.75f;
    private float playerPoints;
    private Vector2 lastPosition;
    private List<GameObject> platforms = new List<GameObject>();
    private List<System.Type> platformEffects = new List<System.Type>
    {
        typeof(MovingPlatformLeftToRight),
        typeof(MovingPlatform),
        typeof(ExpandingPlatform),
        typeof(FallingPlatform)
    };


    void Start()
    {
        CalculateMaxJumpDistance(0, 3);
        playerStart = player.transform;
        cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        GeneratePlatforms();
        lastCheckedPoints = 0;

    }

    void Update()
    {
        playerPoints = myPlayerController.GetPoints();

        if (playerPoints > 5 && playerPoints > lastCheckedPoints)
        {
            AddPlatform();
            lastCheckedPoints = playerPoints;
        }

        if (GameManager.Instance.score >= 5 && platforms.Count > 10)
        {
            RemoveOldestPlatform();

        }
    }

    private void GeneratePlatforms()
    {
        lastPosition = playerStart.position;

        for (int i = 0; i < 10; i++)
        {
            AddPlatform();
        }
    }

    private bool PreventToClose(Vector2 newPosition)
    {
        int rightCount = 0;
        int positionsToCheck = Mathf.Min(3, platforms.Count);

        float minDistanceX = playerWidth + 1;
        float maxDistanceX = cameraWidth / 2;
        float minDistanceY = playerHeight + 5;

        for (int i = platforms.Count - 1; i >= platforms.Count - positionsToCheck; i--)
        {
            Vector2 previousPosition = platforms[i].transform.position;

            if (newPosition.x != previousPosition.x
                && (newPosition.x < previousPosition.x - minDistanceX || newPosition.x > previousPosition.x + maxDistanceX))
            {
                rightCount++;
            }
        }

        return rightCount == positionsToCheck;
    }

    private void CalculateMaxJumpDistance(float startY, float endY)
    {
        float jumpForce = myPlayerController.jumpForce;
        float gravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
        float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScale);
        maxJumpHeight = (jumpForce * jumpForce) / (2f * gravity);
        maxJumpDistance = Mathf.Sqrt(maxJumpHeight * Mathf.Abs(endY - startY));
    }

    private void AddPlatform()
    {
        float randomX;
        float randomY;
        int safetyCounter = 0;
        count++;

        do
        {
            randomX = Random.Range(mainCamera.transform.position.x - (cameraWidth / 2), mainCamera.transform.position.x + (cameraWidth / 2));
            randomY = (platforms.Count == 0)
                ? lastPosition.y + Random.Range(playerHeight + 3, maxJumpDistance)
                : platforms[platforms.Count - 1].transform.position.y + Random.Range(playerHeight + 3, maxJumpDistance);

            safetyCounter++;
            if (safetyCounter > 1000)
            {
                Debug.LogError("Infinite loop detected in AddPlatform");
                break;
            }
        } while (PreventToClose(new Vector2(randomX, randomY)));

        Vector2 platformPosition = new Vector2(randomX, randomY);
        GameObject newPlatform = Instantiate(platFormType1, platformPosition, Quaternion.identity);
        newPlatform.name = "Platform" + count;
        AddRandomEffect(newPlatform);
        platforms.Add(newPlatform);
    }


    private void RemoveOldestPlatform()
    {
        if (platforms.Count > 0)
        {
            GameObject platformToRemove = platforms[0];
            platforms.RemoveAt(0);
            Destroy(platformToRemove);
        }
    }
    private void AddRandomEffect(GameObject platform)
    {
        if (platformEffects.Count > 0)
        {
            int randomIndex = Random.Range(0, platformEffects.Count);
            System.Type effectType = platformEffects[randomIndex];

            // Aktivera effekten p� plattformen
            Platform platformScript = platform.GetComponent<Platform>();
            if (platformScript != null)
            {
                if (playerPoints > 5 && playerPoints < 10 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(platformEffects[0] , 1);
                }
                else if (playerPoints > 10 && playerPoints < 15 && playerPoints > lastCheckedPoints)
                {
                    platformScript.ActivateEffect(platformEffects[1], 2);
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
                else
                {
                    platformScript.ActivateEffect(null, 0);
                }
            }
        }
    }

}