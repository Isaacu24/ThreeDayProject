using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;

    public int currentJumpCount = 0;
    public bool isGrounded = false;
    public bool onceJumpRayCheck = false;

    [HideInInspector]
    public Rigidbody2D rigidbody;

    protected CapsuleCollider2D capsulleCollider;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected ParticleSystem particle;

    public delegate void PlayerDeadedDelegate();
    public PlayerDeadedDelegate OnPlayerDeaded;

    [Header("Status")]
    protected int curHP;
    protected int curCoin;
    protected int curExp;
    protected float curDef;
    protected float moveSpeed;
    protected int jumpCount;
    protected float jumpForce;

    float pretmpY;
    float groundCheckUpdateTic = 0;
    float groundCheckUpdateTime = 0.01f;

    public virtual void Jump()
    {
        anim.Play("Jump");

        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        onceJumpRayCheck = true;
        isGrounded = false;

        currentJumpCount++;
    }

    protected virtual void Die()
    {
        anim.enabled = false; 

        if (null != OnPlayerDeaded)
        {
            OnPlayerDeaded();
        }
    }

    protected void GroundCheckUpdate()
    {
        if (false == onceJumpRayCheck)
        {
            return;
        }

        groundCheckUpdateTic += Time.deltaTime;

        if (groundCheckUpdateTic > groundCheckUpdateTime)
        {
            groundCheckUpdateTic = 0;

            if (0 == pretmpY)
            {
                pretmpY = transform.position.y;
                return;
            }

            float reY = transform.position.y - pretmpY;

            if (reY <= 0)
            {
                if (true == isGrounded)
                {
                    LandingEvent();
                    onceJumpRayCheck = false;
                }

                else
                {
                    Debug.Log("Air");
                }
            }
            pretmpY = transform.position.y;
        }
    }

    protected abstract void LandingEvent();
}
