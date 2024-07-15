using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Lägg till detta för att använda TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText; // Ändra från Text till TextMeshProUGUI

    void Awake()
    {
        // Singleton pattern to ensure there's only one GameManager instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
