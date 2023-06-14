using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;

    private CapsuleCollider2D capsulleCollider;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particle;

    private int attackPower;

    public int AttackPower
    {
        get { return attackPower; }
    }

    private void Start()
    {
        capsulleCollider = GetComponent<CapsuleCollider2D>();
        capsulleCollider.enabled = false;

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if(null != spriteRenderer)
        {
            spriteRenderer.sprite = weaponData.Sprite;
        }

        transform.GetChild(0).transform.localPosition = weaponData.WeaponPivot;
        transform.GetChild(0).transform.localRotation = weaponData.WeaponFilp;

        particle = transform.Find("Weapon_AfterImage").GetComponent<ParticleSystem>();
        Renderer renderer = particle.GetComponent<ParticleSystemRenderer>();
        particle.Pause();

        particle.gameObject.transform.localPosition = weaponData.ParticlePivot;
        particle.gameObject.transform.localRotation= weaponData.ParticleFilp;

        if (null != renderer)
        {
            Material weaponMat = weaponData.Material;
            renderer.material = weaponMat;
        }

        MainModule mainModule = particle.main;
        mainModule.startSizeX = new MinMaxCurve(weaponData.StartSize.x);
        mainModule.startSizeY = new MinMaxCurve(weaponData.StartSize.y);
        mainModule.startSizeZ = new MinMaxCurve(weaponData.StartSize.z);

        ColorOverLifetimeModule colorOverLifetimeModule = particle.colorOverLifetime;
        colorOverLifetimeModule.color = new MinMaxGradient(weaponData.Color);

        attackPower = weaponData.AttackPower;
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
