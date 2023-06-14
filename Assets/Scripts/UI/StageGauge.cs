using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageGauge : MonoBehaviour
{
    private int stagePoint;
    private TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
        pointText = GetComponent<TextMeshProUGUI>();
    }

    public void CountingPoint(int point)
    {
        pointText.text = point.ToString();
    }
}
