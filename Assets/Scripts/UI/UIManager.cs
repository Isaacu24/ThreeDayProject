using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private StageGauge stageGauge;

    [SerializeField]
    private HeartGauge heartGauge;

    [SerializeField]
    private CoinGauge coinGauge;

    [SerializeField]
    private Image damageEffect;

    [SerializeField]
    private Image gameover;

    private bool isMenuActive;

    private void Start()
    {
        Knight.Instance.OnChangeHP += ChangeHP;
        Knight.Instance.OnColletCoin += CollectCoin;
        Knight.Instance.OnIncreaseStagePoint += IncreaseStagePoint;

        Knight.Instance.OnPlayerDeaded += OnEnableGameOver;
    }

    private void Update()
    {
        InputCheck();

        if (null != Knight.Instance)
        {
            if (true == Knight.Instance.IsHit)
            {
                PlusDamageEffectAlpha();
                
                Debug.Log("Plus");
            }

            else if(false == Knight.Instance.IsHit)
            {
                MinusDamageEffectAlpha();

                Debug.Log("Minus");
            }
        }
    }

    void InputCheck()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (true == isMenuActive) 
            {
                DisabledMenu();
            }

            else
            {
                OnEnableMenu();
            }
        }
    }

    public void OnEnableMenu()
    {
        if (null == menu)
        {
            return;
        }

        Time.timeScale = 0.0f;

        menu.gameObject.SetActive(true);
        isMenuActive = true;
    }

    public void DisabledMenu()
    {
        if (null == menu)
        {
            return;
        }
        
        Time.timeScale = 1.0f;

        menu.gameObject.SetActive(false);
        isMenuActive = false;
    }

    public void OnEnableGameOver()
    {
        if (null == gameover)
        {
            return;
        }

        Time.timeScale = 0.0f;
        gameover.gameObject.SetActive(true);
    }

    public void DisabledGameOver()
    {
        if (null == gameover)
        {
            return;
        }

        gameover.gameObject.SetActive(false);
        OnEnableMenu();
    }

    public void ChangeHP(int value)
    {
        //감소
        if (value < heartGauge.Count)
        {
            heartGauge.PopHeart(value - heartGauge.Count);
        }

        //증가
        else
        {
            heartGauge.AddHeart(value - heartGauge.Count);
        }
    }

    private void PlusDamageEffectAlpha()
    {
        Color color = damageEffect.color;
        color.a += (Time.deltaTime * 0.25f);
        color.a = Mathf.Clamp(color.a, 0.0f, 1.0f);
        damageEffect.color = color;
    }

    private void MinusDamageEffectAlpha()
    {
        Color color = damageEffect.color;
        color.a -= (Time.deltaTime);
        color.a = Mathf.Clamp(color.a, 0.0f, 1.0f);
        damageEffect.color = color;
    }

    public void CollectCoin(int value)
    {
        coinGauge.CountingCoin(value);
    }

    public void IncreaseStagePoint(int value)
    {
        stageGauge.CountingPoint(value);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
