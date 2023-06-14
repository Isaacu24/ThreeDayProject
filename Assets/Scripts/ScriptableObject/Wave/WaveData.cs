using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Scriptable Object/Wave Data", order = 1)]
public class WaveData : ScriptableObject
{
    [SerializeField]
    private List<FallingObjectData> faillingObjectDatas;

    public List<FallingObjectData> FallingObjectDatas
    {
        get { return faillingObjectDatas; }
    }

    [SerializeField]
    private SkillData skillData;

    [SerializeField]
    private Vector3 spawnPoint;

    public Vector3 SpawnPoint
    {
        get { return spawnPoint; }
    }

    [SerializeField]
    private Vector3 spawnOffset;

    public Vector3 SpawnOffset
    {
        get { return spawnOffset; }
    }

    [SerializeField]
    private float waveInterval;

    public float WaveInterval
    {
        get { return waveInterval; }
    }
}
