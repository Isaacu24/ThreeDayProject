using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int maxHP;

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    [SerializeField]
    private int coin;
    
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }

    [SerializeField]
    private int def;

    public int Defense
    {
        get { return def; }
        set { def = value; }
    }

    [SerializeField]
    private float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    [SerializeField]
    private int jumpCount;

    public int JumpCount
    {
        get { return jumpCount; }
        set { jumpCount = value; }
    }

    [SerializeField]
    private float jumpForce;

    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    [SerializeField]
    private Sprite deathSprite;

    public Sprite DeathSprite
    {
        get { return deathSprite; }
        set { deathSprite = value; }
    }
}
