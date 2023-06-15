using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private FallingObjectData data;

    private int hp;

    protected SpriteRenderer renderer;
    protected Animator animator;
    protected Rigidbody2D rigidbody;
    protected CapsuleCollider2D capsulleCollider;

    [SerializeField]
    private Coin coinPrefab;

    private int damage;

    public void Initialize(FallingObjectData inData)
    {
        data = inData;

        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        capsulleCollider = GetComponent<CapsuleCollider2D>();

        renderer.sprite = data.Sprite;
        animator.runtimeAnimatorController = data.AnimController;

        hp = data.MaxMP;
        
        capsulleCollider.offset = data.ColliderOffset;
        capsulleCollider.size = data.ColliderSize;

        rigidbody.mass = data.Mass;
        rigidbody.gravityScale = data.GravityScale;
    }

    private void Update()
    {
        //�浹�� ���� ���� ����ġ
        rigidbody.gravityScale += (Time.deltaTime * 0.01f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true == other.CompareTag("Weapon"))
        {
            if(null != other.GetComponent<Weapon>())
            {
                damage = other.GetComponent<Weapon>().AttackPower;

                Color hitColor = new Color(1.0f, 0.47f, 0.47f, 1.0f);
                renderer.color = hitColor;
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
            renderer.color = Color.white;
        }
    }

    public void CalculateDamage()
    {
        hp -= damage;

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
        Knight.Instance.StagePoint = Knight.Instance.StagePoint + data.Exp;

        gameObject.SetActive(false);

        for (int i = 0; i < data.Coin; ++i)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
