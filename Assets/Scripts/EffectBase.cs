using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
