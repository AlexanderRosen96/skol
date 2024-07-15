using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public List<Sprite> backgrounds; // Lista med bakgrundsbilder
    public SpriteRenderer backgroundRenderer; // SpriteRenderer f�r bakgrunden
    public int[] scoreThresholds; // Po�ngniv�er vid vilka bakgrunden �ndras
    public Transform cameraTransform; // Referens till kamerans transform

    private int currentBackgroundIndex = 0;
    private Vector3 initialBackgroundPosition;

    void Start()
    {
        if (backgrounds.Count > 0)
        {
            backgroundRenderer.sprite = backgrounds[0];
        }
        initialBackgroundPosition = backgroundRenderer.transform.position;
    }

    void Update()
    {
        // Uppdatera bakgrundens position f�r att f�lja kameran
        Vector3 newBackgroundPosition = new Vector3(initialBackgroundPosition.x, cameraTransform.position.y, initialBackgroundPosition.z);
        backgroundRenderer.transform.position = newBackgroundPosition;

        // Kolla po�ngen och byt bakgrund om n�dv�ndigt
        int score = GameManager.Instance.score;
        for (int i = 0; i < scoreThresholds.Length; i++)
        {
            if (score >= scoreThresholds[i] && currentBackgroundIndex < i)
            {
                ChangeBackground(i);
            }
        }
    }

    void ChangeBackground(int index)
    {
        if (index >= 0 && index < backgrounds.Count)
        {
            backgroundRenderer.sprite = backgrounds[index];
            currentBackgroundIndex = index;
        }
    }
}
