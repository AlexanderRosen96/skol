using System.Collections;
using UnityEngine;

public class ExpandingPlatform : DynamicEffectController
{
    private float expandSpeed = 2f;
    private float maxScale = 2f; 
    private float minScale = 0.5f; 
    private Vector3 initialScale; 


    void Start()
    {
        toStart();
        initialScale = transform.localScale;

    }

    void Update()
    {
        gameObject.tag = "PlatformExpanding";
        float scaleFactor = (Mathf.Sin(Time.time * expandSpeed) + 1) / 2; 
        float newScaleX = Mathf.Lerp(minScale, maxScale, scaleFactor);
        transform.localScale = new Vector3(newScaleX, initialScale.y, initialScale.z);
    }
    public void ActivateEffect(bool activate)
    {
        enabled = activate;
    }
}
