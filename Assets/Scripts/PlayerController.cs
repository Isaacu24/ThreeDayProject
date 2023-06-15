using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

enum SOUND_TYPE
{
    JUMP,
    SKILL,
    HIT,
    ATTACK,
    DEAD
}

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected PlayerData playerData;

    public int currentJumpCount = 0;
    public bool isGrounded = false;
    public bool onceJumpRayCheck = false;

    [HideInInspector]
    public Rigidbody2D curRigidbody;

    protected AudioSource curAudio;

    [SerializeField]
    protected AudioClip[] curAudiocilps;

    protected CapsuleCollider2D capsulleCollider;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected ParticleSystem particle;

    public delegate void PlayerDeadedDelegate();
    public PlayerDeadedDelegate OnPlayerDeaded;

    [Header("Status")]
    protected int curHP;
    protected int curCoin;
    protected int curPoint;
    protected float curDef;
    protected float moveSpeed;
    protected int jumpCount;
    protected float jumpForce;

    float pretmpY;
    float groundCheckUpdateTic = 0;
    float groundCheckUpdateTime = 0.01f;

    protected bool isHit;
    protected bool isDead;

    public virtual void Jump()
    {
        anim.Play("Jump");
        curAudio.PlayOneShot(curAudiocilps[(int)SOUND_TYPE.JUMP]);

        curRigidbody.velocity = new Vector2(0, 0);
        curRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        onceJumpRayCheck = true;
        isGrounded = false;

        currentJumpCount++;
    }

    protected virtual void Die()
    {
        curAudio.PlayOneShot(curAudiocilps[(int)SOUND_TYPE.DEAD]);

        isDead = true;
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
            }
            pretmpY = transform.position.y;
        }
    }

    protected abstract void LandingEvent();
}
