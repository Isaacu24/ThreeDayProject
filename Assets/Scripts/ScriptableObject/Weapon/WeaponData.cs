using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data", order = 3)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    public Sprite Sprite
    {
        get { return sprite; }
    }

    [SerializeField]
    private Vector3 weaponPivot;

    public Vector3 WeaponPivot
    {
        get { return weaponPivot; }
    }

    [SerializeField]
    private Quaternion weaponFilp;

    public Quaternion WeaponFilp
    {
        get { return weaponFilp; }
    }

    [SerializeField]
    private Material material;

    public Material Material
    {
        get { return material; }
    }

    [SerializeField]
    private Color color;

    public Color Color
    {
        get { return color; }
    }

    [SerializeField]
    private Vector3 startSize;

    public Vector3 StartSize
    {
        get { return startSize; }
    }

    [SerializeField]
    private Vector3 particlePivot;

    public Vector3 ParticlePivot
    {
        get { return particlePivot; }
    }

    [SerializeField]
    private Quaternion particleFilp;

    public Quaternion ParticleFilp
    {
        get { return particleFilp; }
    }

    [SerializeField]
    private int attackPower;

    public int AttackPower
    {
        get { return attackPower; }
    }
}
