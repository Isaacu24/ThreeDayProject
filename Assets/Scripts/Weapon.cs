using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private List<WeaponData> weaponDatas;

    [SerializeField]
    private int anchor;

    private CapsuleCollider2D capsulleCollider;
    private AudioSource curAudio;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;

    private int attackPower;

    public int AttackPower
    {
        get { return attackPower; }
    }

    private void Start()
    {
        SetWeaponData();

        curAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            --anchor;
            anchor = Mathf.Clamp(anchor, 0, weaponDatas.Count - 1);

            SetWeaponData();
            curAudio.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ++anchor;
            anchor = Mathf.Clamp(anchor, 0, weaponDatas.Count - 1);

            SetWeaponData();
            curAudio.Play();
        }
    }

    void SetWeaponData()
    {
        capsulleCollider = GetComponent<CapsuleCollider2D>();
        capsulleCollider.enabled = false;

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (null != spriteRenderer)
        {
            spriteRenderer.sprite = weaponDatas[anchor].Sprite;
        }

        transform.GetChild(0).transform.localPosition = weaponDatas[anchor].WeaponPivot;
        transform.GetChild(0).transform.localRotation = weaponDatas[anchor].WeaponFilp;

        particle = transform.Find("Weapon_AfterImage").GetComponent<ParticleSystem>();
        Renderer renderer = particle.GetComponent<ParticleSystemRenderer>();
        particle.Pause();

        particle.gameObject.transform.localPosition = weaponDatas[anchor].ParticlePivot;
        particle.gameObject.transform.localRotation= weaponDatas[anchor].ParticleFilp;

        if (null != renderer)
        {
            Material weaponMat = weaponDatas[anchor].Material;
            renderer.material = weaponMat;
        }

        MainModule mainModule = particle.main;
        mainModule.startSizeX = new MinMaxCurve(weaponDatas[anchor].StartSize.x);
        mainModule.startSizeY = new MinMaxCurve(weaponDatas[anchor].StartSize.y);
        mainModule.startSizeZ = new MinMaxCurve(weaponDatas[anchor].StartSize.z);

        ColorOverLifetimeModule colorOverLifetimeModule = particle.colorOverLifetime;
        colorOverLifetimeModule.color = new MinMaxGradient(weaponDatas[anchor].Color);

        attackPower = weaponDatas[anchor].AttackPower;
    }

    public void AttackStart()
    {
        capsulleCollider.enabled = true;
        particle.Play();
    }

    public void AttackEnd()
    {
        capsulleCollider.enabled = false;

        particle.Clear();
        particle.Pause();
    }
}
