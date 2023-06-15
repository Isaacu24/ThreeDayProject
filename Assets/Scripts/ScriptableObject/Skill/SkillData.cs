using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = 4)]
public class SkillData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    public Sprite Sprite
    {
        get { return sprite; }
    }
}
