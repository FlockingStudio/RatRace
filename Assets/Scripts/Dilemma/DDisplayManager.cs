using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static GameManager;

public class DDisplayManager : MonoBehaviour
{ 
    private int DiceIndex = -1;
    private int MoneyImpact = 0;
    void Start()
    {
        Initialize();
    }

    void Update()
    {
    }

    private void Initialize()
    {
        string text;
        if (Player.Instance.GetEventDifficulty() == Difficulty.EASY)
        {
            text = GameManager.Instance.easyDilemmaPool.GetRandom();
        }
        else
        {
            text = GameManager.Instance.hardDilemmaPool.GetRandom();
        }

        string[] dilemmaInfo = text.Split(",");

        TextMeshProUGUI dilemmaText = transform.Find("DilemmaText").GetComponent<TextMeshProUGUI>();
        dilemmaText.SetText(dilemmaInfo[0]);

        TextMeshProUGUI option1Text = transform.Find("TopButton/Text").GetComponent<TextMeshProUGUI>();
        option1Text.SetText(dilemmaInfo[1]);

        TextMeshProUGUI option2Text = transform.Find("BottomButton/Text").GetComponent<TextMeshProUGUI>();
        option2Text.SetText(dilemmaInfo[3]);

        MoneyImpact = int.Parse(dilemmaInfo[2]);
        DiceIndex = int.Parse(dilemmaInfo[4]);
    }

    public void OnTopButtonPressed()
    {
        if (MoneyImpact > 0)
        {
            Player.Instance.AddMoney(MoneyImpact);
        } else
        {
            Player.Instance.SubtractMoney(-MoneyImpact);
        }

        if (Player.Instance.Turn < 1)
        {
            GameManager.Instance.OpenLeaderBoard();
        }
    }

    public void OnBottomButtonPressed()
    {
        Player.Instance.DicePrices[DiceIndex] -= 10;

        if (Player.Instance.Turn < 1)
        {
            GameManager.Instance.OpenLeaderBoard();
        }
    }
}
