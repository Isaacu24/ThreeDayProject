using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider2D collider;
    private Rigidbody2D rigidbody;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true == other.CompareTag("Ground"))
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
