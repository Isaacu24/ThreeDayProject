using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private ParticleSystem particle;

    [SerializeField]
    private GameObject weaponArm;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (null != particle)
        {
            ParticleSystem.MainModule main = particle.main;

            if (main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
                main.startRotation = -weaponArm.transform.eulerAngles.z * Mathf.Deg2Rad;
            }
        }
    }
}
