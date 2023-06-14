using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private Image heart;

    [SerializeField]
    private Image heartFrame;

    private bool isFill = true;

    public bool IsFill
    {
        get { return isFill; }
    }

    private void OnEnable()
    {
        heart = transform.Find("Heart").GetComponent<Image>();
        heartFrame = transform.Find("HeartFrame").GetComponent<Image>();
    }

    public void VacateHeart()
    {
        if (heart != null)
        {
            isFill = false;
            heart.gameObject.SetActive(false);
        }
    }

    public void FillHeart()
    {
        if (heart != null)
        {
            isFill = true;
            heart.gameObject.SetActive(true);
        }
    }
}
