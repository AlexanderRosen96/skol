using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public List<Sprite> backgrounds; // Lista med bakgrundsbilder
    public SpriteRenderer backgroundRenderer; // SpriteRenderer för bakgrunden
    public int[] scoreThresholds; // Poängnivåer vid vilka bakgrunden ändras
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
        // Uppdatera bakgrundens position för att följa kameran
        Vector3 newBackgroundPosition = new Vector3(initialBackgroundPosition.x, cameraTransform.position.y, initialBackgroundPosition.z);
        backgroundRenderer.transform.position = newBackgroundPosition;

        // Kolla poängen och byt bakgrund om nödvändigt
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
