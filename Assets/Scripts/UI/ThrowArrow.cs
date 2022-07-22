using UnityEngine;
using System.Collections;

public class ThrowArrow : MonoBehaviour
{
    void Start()
    {
        Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);

        float forceMagnitude = 10.0f;

        Vector3 force = forceMagnitude * forceDirection;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
