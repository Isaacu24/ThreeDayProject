using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public static CameraController Instance;

    public GameObject Target;
    public int Smoothvalue =2;
    public float PosY = 1;

    public Coroutine my_co;

    void Start()
    {
     
    }

    void Update()
    {
        Vector3 Targetpos = new(Target.transform.position.x, Target.transform.position.y + PosY, -100.0f);
        transform.position = Vector3.Lerp(transform.position, Targetpos, Time.deltaTime * Smoothvalue);

        if(-0.25f > transform.position.y)
        {
            Vector3 Pos = transform.position;
            Pos.y = -0.25f;
            transform.position = Pos;
        }
    }
}
