using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private List<WaveData> waveDatas;
    private List<Wave> waves;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private AnimationCurve curve;

    private FallingObject prevLastObject;
    private int anchor;
    private int stageCount;
    private bool isWaveStart;

    private void Start()
    {
        //curve = GetComponent<AnimationCurve>();

        waves = new List<Wave>();

        for (int i = 0; i < waveDatas.Count; ++i)
        {
            GameObject newObject = Instantiate(prefab);

            if (null != newObject.GetComponent<Wave>())
            {
                Wave wave = newObject.GetComponent<Wave>();
                wave.Initialize(waveDatas[i]);
                waves.Add(wave);
            }
        }

        prevLastObject = waves[0].LastObject;
    }

    public void WaveClear()
    {
        //Game Clear
        anchor = 0;
        prevLastObject = waves[0].LastObject;

        for (int i = 0; i < waves.Count; ++i)
        {
            waves[i].DisabledFallObjects();
        }

        if (null != Knight.Instance)
        {
            Knight.Instance.ReSetting();
        }

        //코루틴 중지 전 StopCoroutine이 호출되면 코루틴이 멈추지 않을 수 있음!
        StopAllCoroutines();
    }

    public void ReStartWave()
    {
        if (true == isWaveStart)
        {
            isWaveStart = false;
            WaveClear();

            Invoke("StartWave", 1.0f);
        }
    }

    // Wave 시작
    public void StartWave()
    {
        if(false == isWaveStart)
        {
            isWaveStart = true;
            StartCoroutine(SpawnWaves());
        }
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            if (false == prevLastObject.isActiveAndEnabled) 
            {
                waves[anchor].GetComponent<Wave>().FallObjects();
                prevLastObject = waves[anchor].LastObject;

                float Poweroffset = curve.Evaluate(stageCount);

                anchor++;
                waves[anchor - 1].ReSetting(Poweroffset);

                if (waves.Count - 1 < anchor)
                {
                    ++stageCount;
                    anchor = 0;

                    Knight.Instance.StagePoint += (stageCount * 10);
                }

                yield return new WaitForSeconds(waves[anchor].WaveInterval);
            }

            yield return null;
        }
    }
}
