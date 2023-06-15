using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D curRigidbody;
    private AudioSource curAudio;

    private void OnEnable()
    {
        curRigidbody = GetComponent<Rigidbody2D>();
        curAudio = GetComponent<AudioSource>();    

        curRigidbody.velocity = new Vector2(0, 0);

        Vector2 dir;
        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = Random.Range(0.0f, 1.0f);

        float force = Random.Range(1.0f, 7.5f);

        curRigidbody.AddForce(dir * force, ForceMode2D.Impulse);
        curAudio.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true == other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
