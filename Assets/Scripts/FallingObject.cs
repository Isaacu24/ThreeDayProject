using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private FallingObjectData data;

    private int hp;

    protected SpriteRenderer curRenderer;
    protected Animator animator;
    protected AudioSource curAudio;
    protected Rigidbody2D curRigidbody;
    protected CapsuleCollider2D capsulleCollider;

    [SerializeField]
    private Coin coinPrefab;

    [SerializeField]
    private EffectBase Effect;

    private int damage;

    public void Initialize(FallingObjectData inData)
    {
        data = inData;

        curRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        curAudio = GetComponent<AudioSource>();
        curRigidbody = GetComponent<Rigidbody2D>();
        capsulleCollider = GetComponent<CapsuleCollider2D>();

        curRenderer.sprite = data.Sprite;
        animator.runtimeAnimatorController = data.AnimController;

        hp = data.MaxMP;
        
        capsulleCollider.offset = data.ColliderOffset;
        capsulleCollider.size = data.ColliderSize;

        curRigidbody.mass = data.Mass;
        curRigidbody.gravityScale = data.GravityScale;
    }

    private void Update()
    {
        //충돌을 위한 무게 가중치
        curRigidbody.gravityScale += (Time.deltaTime * 0.01f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true == other.CompareTag("Weapon"))
        {
            if(null != other.GetComponent<Weapon>())
            {
                damage = other.GetComponent<Weapon>().AttackPower;

                Color hitColor = new Color(1.0f, 0.47f, 0.47f, 1.0f);
                curRenderer.color = hitColor;
            }

            CalculateDamage();
        }

        else if (true == other.CompareTag("SkillPlayer"))
        {
            Die();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (true == other.CompareTag("Weapon"))
        {
            curRenderer.color = Color.white;
        }

        else if (true == other.CompareTag("SkillPlayer"))
        {
            Die();
        }
    }

    public void CalculateDamage()
    {
        hp -= damage;

        curAudio.Play();

        if (0 >= hp)
        {
            Die();
        }
    }
    
    public void ReSetting(float value)
    {
        hp = (int)(data.MaxMP + value);
        damage = 0;
    }

    private void Die()
    {
        Knight.Instance.Coin = data.Coin;
        Knight.Instance.StagePoint = data.StagePoint;

        gameObject.SetActive(false);

        for (int i = 0; i < data.Coin; ++i)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        Instantiate(Effect, transform.position, Quaternion.identity);
    }
}
