using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static GameManager;

public class DDisplayManager : MonoBehaviour
{
    //dilemma csv order: body text, button text 1, button text 2, money effect of 1, turn effect of 1, money effect of 2, turn effect of 2, image name
    public TextMeshProUGUI dilemmaText;
    public Image dilemmaImage;
    public TextAsset dilemmaCSV;
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public GameObject resultWindowPrefab;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private static CSVPool dilemmaPool;
    private string[] dilemmaInfo;

    void Start()
    {
        // Create pool on first run
        if (dilemmaPool == null)
        {
            dilemmaPool = new CSVPool(dilemmaCSV);
        }

        LoadRandomDilemma();
        // Disables the left button if the cost of pressing it is more than the player has
        if (Player.Instance.GetMoney() + int.Parse(dilemmaInfo[3]) < 0)
        {
            leftButton.interactable = false;
        }
        // Disables the right button if the cost of pressing it is more than the player has
        if (Player.Instance.GetMoney() + int.Parse(dilemmaInfo[5]) < 0)
        {
            rightButton.interactable = false;
        }
        // Plays the correct track for the dilemma
        SoundManager.Instance.SwitchBackgroundMusic(Stage.dilemma);
    }

    private void LoadRandomDilemma()
    {
        // Get random dilemma from pool
        string selectedDilemma = dilemmaPool.GetRandom();

        // Parse the selected dilemma
        dilemmaInfo = selectedDilemma.Split(";");

        //Set UI elements
        dilemmaText.SetText(dilemmaInfo[0]);
        choice1.SetText(dilemmaInfo[1]);
        choice2.SetText(dilemmaInfo[2]);

        //Set the dilemma image
        dilemmaImage.sprite = Resources.Load<Sprite>(dilemmaInfo[7].Trim());
    }

    void OnDestroy()
    {
        if (Player.Instance != null && Player.Instance.Turn <= 0 && GameManager.Instance != null)
        {
            GameManager.Instance.IsDayOver = true;
            GameManager.Instance.OpenGameOver();
        }
    }

    public void LeftButtonClick()
    {
        int moneyCost = int.Parse(dilemmaInfo[3]);
        int turnCost = int.Parse(dilemmaInfo[4]);

        HandleButtonClick(moneyCost, turnCost, true);
    }

    public void RightButtonClick()
    {
        int moneyCost = int.Parse(dilemmaInfo[5]);
        int turnCost = int.Parse(dilemmaInfo[6]);

        HandleButtonClick(moneyCost, turnCost, false);
    }

    private void HandleButtonClick(int moneyCost, int turnCost, bool leftChoice)
    {
        leftButton.enabled = false;
        rightButton.enabled = false;
        
        if (moneyCost > 0)
        {
            Player.Instance.AddMoney(moneyCost);
        }
        else if (moneyCost < 0)
        {
            Player.Instance.SubtractMoney(-moneyCost);
        }

        if (turnCost > 0)
        {
            Player.Instance.AddTurn(turnCost);
        }
        else if (turnCost < 0)
        {
            Player.Instance.SubtractTurn(-turnCost);
        }

        ShowResult(leftChoice);
    }

    private void ShowResult(bool leftChoice)
    {
        if (resultWindowPrefab == null)
        {
            Debug.LogError("resultWindowPrefab is not assigned in the Inspector!");
            return;
        }

        GameObject resultWindow = Instantiate(resultWindowPrefab, GameObject.Find("Screen").transform);
        TextMeshProUGUI resultText = resultWindow.GetComponentInChildren<TextMeshProUGUI>();

        if (resultText != null)
        {
            if (leftChoice)
            {
                resultText.text = dilemmaInfo[8];
            }
            else
            {
                resultText.text = dilemmaInfo[9];
            }
        }

        Button resultButton = resultWindow.GetComponentInChildren<Button>();
        if (resultButton != null)
        {
            resultButton.onClick.AddListener(OpenMap);
        }
    }

    private void OpenMap()
    {
        GameManager.Instance.OpenMap();
    }
}
