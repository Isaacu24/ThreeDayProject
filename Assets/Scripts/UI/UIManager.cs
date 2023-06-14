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

        menu.gameObject.SetActive(true);
        isMenuActive = true;
    }

    public void DisabledMenu()
    {
        if (null == menu)
        {
            return;
        }

        menu.gameObject.SetActive(false);
        isMenuActive = false;
    }

    public void OnEnableGameOver()
    {
        if (null == gameover)
        {
            return;
        }

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
        if(0 < value)
        {
            heartGauge.AddHeart(value);
        }

        else 
        {
            heartGauge.PopHeart(value);
        }
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
