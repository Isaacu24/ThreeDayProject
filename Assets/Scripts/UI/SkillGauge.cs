using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Knight;

public class SkillGauge : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    
    [SerializeField]
    private float ratio = 0.05f;

    private void Start()
    {
        if (null != slider)
        {
            slider.value = 0;
        }

        Knight.Instance.OnIncreaseStagePoint += IncreaseSkillPoint;
        Knight.Instance.OnSkillEndedDelegate += SkillPointZero;
    }

    private void IncreaseSkillPoint(int inPoint, int value) 
    {
        slider.value += value * ratio;

        if (slider.maxValue <= slider.value
            && null != Knight.Instance)
        {
            Image image = slider.transform.GetChild(0).GetChild(0).GetComponent<Image>();

            if (null != image)
            {
                Color color = image.color;
                color = new Color(0.0f, 0.1f, 0.93f, 1.0f);
                image.color = color;
            }

            Knight.Instance.CanSkill = true;
        }
    }

    private void SkillPointZero()
    {
        slider.value = 0;

        Image image = slider.transform.GetChild(0).GetChild(0).GetComponent<Image>();

        if (null != image)
        {
            Color color = image.color;
            color = new Color(0.0f, 0.53f, 0.91f, 1.0f);
            image.color = color;
        }
    }
}
