using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetUI : MonoBehaviour
{
    private BoxCollider2D boxColider;

    private Rigidbody2D rigidbody;

    private float width;
    private void OnEnable()
    {
        if(TryGetComponent<BoxCollider2D>(out var boxCollider2D))
        {
            boxColider = boxCollider2D;
            width = boxColider.size.x;
        }
        if(TryGetComponent<Rigidbody2D>(out var rigidbody2D))
        {
            rigidbody = rigidbody2D;
        }
    }
}
