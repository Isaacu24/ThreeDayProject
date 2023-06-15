using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject skillGauge;

    [SerializeField]
    private GameObject keys;

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

        HideUI();
    }

    private void Update()
    {
        InputCheck();

        if (null != Knight.Instance)
        {
            if (true == Knight.Instance.IsHit)
            {
                PlusDamageEffectAlpha();
            }

            else if(false == Knight.Instance.IsHit)
            {
                MinusDamageEffectAlpha();
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

    public void HideUI()
    {
        //기타 UI 비활성화
        stageGauge.gameObject.SetActive(false);
        heartGauge.transform.parent.gameObject.SetActive(false);
        skillGauge.SetActive(false);
        keys.SetActive(false);

        Color color = damageEffect.color;
        color.a = 0;
        damageEffect.color = color;
    }

    public void VisibleUI()
    {
        stageGauge.gameObject.SetActive(true);
        heartGauge.transform.parent.gameObject.SetActive(true);
        skillGauge.SetActive(true);
        keys.SetActive(true);
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

        HideUI();
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

        VisibleUI();
    }

    public void OnEnableGameOver()
    {
        if (null == gameover)
        {
            return;
        }

        Time.timeScale = 0.0f;
        gameover.gameObject.SetActive(true);

        HideUI();
    }

    public void DisabledGameOver()
    {
        if (null == gameover)
        {
            return;
        }

        gameover.gameObject.SetActive(false);
        OnEnableMenu();

        VisibleUI();
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

    public void IncreaseStagePoint(int inPoint, int value)
    {
        stageGauge.CountingPoint(inPoint);
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
