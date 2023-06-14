using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartGauge : MonoBehaviour
{
    private List<Heart> hearts;

    [SerializeField]
    private GameObject prefab;

    private Vector3 offset;

    private void Start()
    {
        hearts = new List<Heart>();
        offset = new Vector3(-30.0f, 0.0f, 0.0f);

        AddHeart(Knight.Instance.HP);
    }

    public void AddHeart(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Heart newHeart = Instantiate(prefab).GetComponent<Heart>();
            hearts.Add(newHeart);

            newHeart.transform.localPosition += (offset * i);
            newHeart.transform.SetParent(transform, false);
        }

        hearts.Reverse();
    }

    public void PopHeart(int count)
    {
        count = Mathf.Abs(count);

        for (int i = 0; i < count; ++i)
        {
            for (int j = 0; j < hearts.Count; ++j)
            {
                if (true == hearts[j].IsFill)
                {
                    hearts[j].VacateHeart();
                    break;
                }
            }
        }
    }
}
