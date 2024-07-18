using UnityEngine;

public class MovingPlatformLeftToRight : DynamicEffectController
{
    private float moveSpeed;
    private float moveDistance;
    private Vector2 startPosition;
    private bool moveRight;
    private bool shouldMove;

    void Awake()
    {
        startPosition = transform.position;
        moveRight = Random.Range(0, 2) == 0;
        shouldMove = Random.Range(0f, 1f) > 0.2f;
        moveSpeed = Random.Range(0.5f, 2f);
        moveDistance = Random.Range(0.5f, 3f);
    }

    void Update()
    {
        if (shouldMove)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            Vector2 newPosition = startPosition;
            float direction = moveRight ? 1 : -1;
            newPosition.x += Mathf.Sin(Time.time * moveSpeed) * moveDistance * direction;
            transform.position = newPosition;
        }
    }
    void Start()
    {
        toStart();

        
    }
        public void ActivateEffect(bool activate)
    {
        enabled = activate;
    }
}
