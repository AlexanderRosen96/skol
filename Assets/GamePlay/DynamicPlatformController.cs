using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformController : MonoBehaviour
{
    protected Rigidbody2D rb; 
    protected Collider2D coll;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        coll = GetComponent<Collider2D>();
        rb.bodyType = RigidbodyType2D.Dynamic; 
    }
}
