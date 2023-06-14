using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FallingObject Data", menuName = "Scriptable Object/FallingObject Data", order = 2)]
public class FallingObjectData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    public Sprite Sprite
    {
        get { return sprite; }
    }

    [SerializeField]
    private RuntimeAnimatorController animController;

    public RuntimeAnimatorController AnimController
    {
        get { return animController; }
    }

    [SerializeField]
    private int maxHP;

    public int MaxMP
    {
        get { return maxHP; }
    }

    //플레이어에게 줄 경험치(StagePoint)
    [SerializeField]
    private int exp;

    public int Exp
    {
        get { return exp; }
    }

    //플레이어에게 줄 코인 수
    [SerializeField]
    private int coin;

    public int Coin
    {
        get { return coin; }
    }

    [SerializeField]
    private Vector2 colliderSize;

    public Vector2 ColliderSize
    {
        get { return colliderSize; }
    }

    [SerializeField]
    private Vector2 colliderOffset;

    public Vector2 ColliderOffset
    {
        get { return colliderOffset; }
    }

    [SerializeField]
    private float mass;

    public float Mass
    {
        get { return mass; }
    }

    [SerializeField]
    private float gravityScale;

    public float GravityScale
    {
        get { return gravityScale; }
    }
}
