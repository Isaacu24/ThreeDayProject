using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;


public class Knight : PlayerController
{
    private static Knight instance = null;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private EffectBase Effect;

    public delegate void ChangeHPDelegate(int inCurHp);
    public ChangeHPDelegate OnChangeHP;

    public delegate void ColletCoinDelegate(int inCurCoin);
    public ColletCoinDelegate OnColletCoin;

    public delegate void IncreaseStagePointDelegate(int inPoint, int value);
    public IncreaseStagePointDelegate OnIncreaseStagePoint;

    public delegate void SkillEndedDelegate();
    public SkillEndedDelegate OnSkillEndedDelegate;

    private bool canSkill;
    private bool useSkill;

    private GameObject curFallingObject = null;
    public bool CanSkill
    {
        get { return canSkill; }
        
        set { canSkill = value; }
    }

    public bool UseSkill
    {
        get { return useSkill; }
    }

    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
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
                if(curHP!=playerData.MaxHP)
                {
                    curAudio.PlayOneShot(curAudiocilps[(int)SOUND_TYPE.HIT]);
                }
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
            return curPoint;
        }

        set
        {
            curPoint += value;

            if (null != OnIncreaseStagePoint)
            {
                OnIncreaseStagePoint(curPoint, value);
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
        curPoint = 0;

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
        curRigidbody = GetComponent<Rigidbody2D>();
        curAudio = GetComponent<AudioSource>();

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
    }

    private void LateUpdate()
    {
        if(null != curFallingObject)
        {
            if(false == curFallingObject.activeSelf)
            {
                IsHit = false;
                curFallingObject = null;
            }
        }
    }

    private void FixedUpdate()
    {
        GroundCheckUpdate();
    }

    public void CheckInput()
    {
        if (false == anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            if (true == canSkill
                && false == useSkill
                && Input.GetKeyDown(KeyCode.A))
            {
                anim.Play("Skill");
                curAudio.PlayOneShot(curAudiocilps[(int)SOUND_TYPE.SKILL]);

                gameObject.tag = "SkillPlayer";
                capsulleCollider.isTrigger = true;

                curRigidbody.velocity = new Vector2(0, 0);
                curRigidbody.AddForce(Vector2.up * (jumpForce * 2.0f), ForceMode2D.Impulse);

                Instantiate(Effect, transform.position, Quaternion.identity);

                useSkill = true;
            }
        }

        if (false == anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                anim.Play("Attack");
                weapon.AttackStart();

                curAudio.PlayOneShot(curAudiocilps[(int)SOUND_TYPE.ATTACK]);
            }

            else
            {
                weapon.AttackEnd();

                if (false == onceJumpRayCheck
                    && false == useSkill)
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

            curFallingObject ??= collision.gameObject;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true == useSkill
            && true == other.CompareTag("Ground"))
        {
            //SkillEnd
            gameObject.tag = "Player";
            capsulleCollider.isTrigger = false;

            useSkill = false;
            canSkill = false;

            OnSkillEndedDelegate?.Invoke();
        }
    }

    public override void Jump()
    {
        if (currentJumpCount < jumpCount)
        {
            base.Jump();
            particle?.Play();
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
        StagePoint = -StagePoint;

        useSkill = false;
        canSkill = false;

        if (null != playerData.DeathSprite)
        {
            spriteRenderer.sprite = null;
        }
    }

    protected override void LandingEvent()
    {
        if (false == anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            || false == anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            anim.Play("Idle");
        }

        particle.Clear();
        particle.Pause();
    }
}
