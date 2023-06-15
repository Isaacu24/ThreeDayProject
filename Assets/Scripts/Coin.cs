using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        Vector2 dir;
        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = Random.Range(0.0f, 1.0f);

        float force = Random.Range(1.0f, 10.0f);

        rigidbody.AddForce(dir * force, ForceMode2D.Impulse);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true == other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
