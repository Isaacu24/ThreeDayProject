using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Knight : PlayerController
{
    private static Knight instance = null;

    [SerializeField]
    private Weapon weapon;

    public delegate void ChangeHPDelegate(int inCurHp);
    public ChangeHPDelegate OnChangeHP;

    public delegate void ColletCoinDelegate(int inCurCoin);
    public ColletCoinDelegate OnColletCoin;

    public delegate void IncreaseStagePointDelegate(int inCurExp);
    public IncreaseStagePointDelegate OnIncreaseStagePoint;

    public bool IsHit
    {
        get { return isHit; }
    }

    public int HP
    {
        get
        {
            return curHP;
        }

        set
        {
            curHP = value;

            if (null != OnChangeHP)
            {
                OnChangeHP(curHP);
            }
        }
    }

    public int Coin
    {
        get
        {
            return curCoin;
        }

        set
        {
            curCoin += value;
            playerData.Coin = curCoin;

            if (null != OnColletCoin)
            {
                OnColletCoin(curCoin);
            }
        }
    }

    public int StagePoint
    {
        get
        {
            return curExp;
        }

        set
        {
            curExp = value;

            if (null != OnIncreaseStagePoint)
            {
                OnIncreaseStagePoint(curExp);
            }
        }
    }

    public static Knight Instance
    {
        get
        {
            if (instance == null)
            {
                Knight knight = new Knight();
                instance = knight;

            }

            return instance;
        }

        private set { instance = value; }
    }

    private void Awake()
    {
        instance = this;

        curHP = playerData.MaxHP;
        curDef = playerData.Defense;
        curExp = 0;

        moveSpeed = playerData.MoveSpeed;
        jumpCount= playerData.JumpCount;
        jumpForce= playerData.JumpForce;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        capsulleCollider  = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        Transform bodyTransform = transform.Find("Body");

        anim = bodyTransform.GetComponent<Animator>();
        spriteRenderer = bodyTransform.GetComponent<SpriteRenderer>();
        particle = bodyTransform.Find("Body_AfterImage").GetComponent<ParticleSystem>();
        particle.Pause();

        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return null;

        Coin = playerData.Coin;
        StopCoroutine(LateStart());
    }

    private void Update()
    {
        CheckInput();

        if (rigidbody.velocity.magnitude > 30)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x - 0.1f, rigidbody.velocity.y - 0.1f);
        }
    }

    public void CheckInput()
    {
        GroundCheckUpdate();

        if (false == anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                anim.Play("Attack");
                weapon.AttackStart();
            }

            else
            {
                weapon.AttackEnd();

                if (false == onceJumpRayCheck)
                {
                    anim.Play("Idle");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (true == collision.collider.CompareTag("FallingObject"))
        {
            curDef -= Time.deltaTime;

            if (true == isGrounded)
            {
                isHit = true;
            }

            if (0.0f >= curDef)
            {
                curDef = playerData.Defense;
                HP = curHP - 1;

                if (0 == curHP)
                {
                    Die();
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (true == collision.collider.CompareTag("FallingObject"))
        {
            isHit = false;
            curDef = playerData.Defense;
        }
    }

    public override void Jump()
    {
        if (currentJumpCount < jumpCount)
        {
            base.Jump();

            if (null != particle)
            {
                particle.Play();
            }
        }
    }

    protected override void Die()
    {
        base.Die();

        weapon.gameObject.SetActive(false);
        isHit = false;

        if (null != playerData.DeathSprite)
        {
            spriteRenderer.sprite = playerData.DeathSprite;
        }
    }

    public void ReSetting()
    {
        anim.Play("Idle");

        anim.enabled = true;
        weapon.gameObject.SetActive(true);

        HP = playerData.MaxHP;
        curDef = playerData.Defense;
        StagePoint = 0;

        if (null != playerData.DeathSprite)
        {
            spriteRenderer.sprite = null;
        }
    }

    protected override void LandingEvent()
    {
        if (false == anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.Play("Idle");
        }

        particle.Clear();
        particle.Pause();
    }
}
