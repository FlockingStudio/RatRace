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

        string[] dilemmaInfo = ParseCSVLine(text);

        TextMeshProUGUI dilemmaText = transform.Find("DilemmaText").GetComponent<TextMeshProUGUI>();
        dilemmaText.SetText(dilemmaInfo[0].Replace("\\n", "\n"));

        TextMeshProUGUI option1Text = transform.Find("TopButton/Text").GetComponent<TextMeshProUGUI>();
        option1Text.SetText(dilemmaInfo[1]);

        TextMeshProUGUI option2Text = transform.Find("BottomButton/Text").GetComponent<TextMeshProUGUI>();
        option2Text.SetText(dilemmaInfo[3]);

        MoneyImpact = int.Parse(dilemmaInfo[2]);
        DiceIndex = int.Parse(dilemmaInfo[4]);
    }

    private string[] ParseCSVLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        string currentField = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(currentField);
                currentField = "";
            }
            else
            {
                currentField += c;
            }
        }

        // Add the last field
        fields.Add(currentField);

        return fields.ToArray();
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
            if (Player.Instance.GetMoney() >= GameManager.Instance.targetMoney)
            {
                DesktopManager.Instance.NextDaySequence();
            } else
            {
                DesktopManager.Instance.EndSequence();
            }
        }
    }

    public void OnBottomButtonPressed()
    {
        switch (DiceIndex)
        {
            case 0:
                Player.Instance.DicePrices[DiceIndex] -= 1;
                break;
            case 1:
                Player.Instance.DicePrices[DiceIndex] -= 2;
                break;
            case 2:
                Player.Instance.DicePrices[DiceIndex] -= 4;
                break;
            default:
                break;
        }

        if (Player.Instance.Turn < 1)
        {
            if (Player.Instance.GetMoney() >= GameManager.Instance.targetMoney)
            {
                DesktopManager.Instance.NextDaySequence();
            }
            else
            {
                DesktopManager.Instance.EndSequence();
            }
        }
    }
}
