using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamicEffectController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D coll;

    protected void toStart()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
