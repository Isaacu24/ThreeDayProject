using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private WaveData data;
    private List<FallingObject> FallingObjects;

    [SerializeField]
    private GameObject prefab;

    private FallingObject lastObject;
    private Vector3 spawnPoint;
    public FallingObject LastObject
    {
        get { return lastObject; }
    }

    public float WaveInterval
    {
        get { return data.WaveInterval; }
    }

    public void Initialize(WaveData inData)
    {
        data = inData;
        spawnPoint = data.SpawnPoint;

        FallingObjects = new List<FallingObject>();
        CreateFallingObjects();
    }

    private void CreateFallingObjects()
    {
        int count = data.FallingObjectDatas.Count;

        for (int i = 0; i < count; ++i)
        {
            GameObject newObject = Instantiate(prefab, spawnPoint, Quaternion.identity);

            if (null == newObject.GetComponent<FallingObject>())
            {
                continue;
            }

            FallingObject fObject = newObject.GetComponent<FallingObject>();
            fObject.Initialize(data.FallingObjectDatas[i]);
            fObject.gameObject.SetActive(false);
            FallingObjects.Add(fObject);

            spawnPoint += data.SpawnOffset;

            if (i == count - 1)
            {
                lastObject = fObject;
            }
        }
    }

    public void FallObjects()
    {
        for (int i = 0; i < FallingObjects.Count; ++i)
        {
            FallingObjects[i].gameObject.SetActive(true);
        }
    }

    public void DisabledFallObjects()
    {
        for (int i = 0; i< FallingObjects.Count; ++i)
        {
            FallingObjects[i].gameObject.SetActive(false);
        }
    }

    public void ReSetting(float value)
    {
        spawnPoint = data.SpawnPoint;

        for (int i = 0; i < FallingObjects.Count; ++i)
        {
            FallingObjects[i].ReSetting(value);
            FallingObjects[i].GetComponent<Transform>().localPosition = spawnPoint;
            spawnPoint += data.SpawnOffset;
        }

        spawnPoint = data.SpawnPoint;
    }

}
