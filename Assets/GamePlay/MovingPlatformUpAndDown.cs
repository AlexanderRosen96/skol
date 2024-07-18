using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformUpAndDown : DynamicEffectController
{
    private float moveSpeed;
    private float moveDistance;
    private Vector2 startPosition;
    private bool shouldMove;
    private bool movingUp;

    private Vector2 direction; 


    private float currentMoveSpeed;

    void Start()
    {
        toStart();

        startPosition = transform.position;
        shouldMove = Random.Range(0f, 1f) > 0.2f;
        moveSpeed = Random.Range(0.5f, 2f);
        moveDistance = Random.Range(0.5f, 3f);
        movingUp = Random.Range(0, 2) == 0;
        currentMoveSpeed = moveSpeed;
        direction = movingUp ? Vector2.up : Vector2.down; 
    }

    void Update()
    {
        if (shouldMove)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            Vector2 newPosition = startPosition;
            newPosition += direction * Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            rb.MovePosition(newPosition);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("invisible") || collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("PlatformExpanding"))
        {
            ReverseDirection();
            currentMoveSpeed = moveSpeed;
            ReverseDirection();
        }
    }
    public void ActivateEffect(bool activate)
    {
        enabled = activate;
    }

    public void ReverseDirection()
    {
        direction = -direction;
        moveSpeed = currentMoveSpeed;
    }
}
