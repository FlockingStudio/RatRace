using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DDisplayManager : MonoBehaviour
{
    //dilemma csv order: body text, button text 1, button text 2, money effect of 1, turn effect of 1, money effect of 2, turn effect of 2, image name
    public TextMeshProUGUI dilemmaText;
    public Image dilemmaImage;
    public TextAsset dilemmaCSV;
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public Button button1;
    public Button button2;

    private static CSVPool dilemmaPool;

    void Start()
    {
        // Create pool on first run
        if (dilemmaPool == null)
        {
            dilemmaPool = new CSVPool(dilemmaCSV);
        }

        LoadRandomDilemma();
        // Plays the correct track for the dilemma
        SoundManager.Instance.playTrackTwo();
    }

    private void LoadRandomDilemma()
    {
        // Get random dilemma from pool
        string selectedDilemma = dilemmaPool.GetRandom();

        // Parse the selected dilemma
        string[] dilemmaInfo = selectedDilemma.Split(";");

        //Set UI elements
        dilemmaText.SetText(dilemmaInfo[0]);
        choice1.SetText(dilemmaInfo[1]);
        choice2.SetText(dilemmaInfo[2]);

        // Gets the DilemmaButton script attached to buttons
        DilemmaButton dilemmaButtonScript1 = button1.GetComponent<DilemmaButton>();
        DilemmaButton dilemmaButtonScript2 = button2.GetComponent<DilemmaButton>();
        dilemmaButtonScript1.SetMoneyCost(int.Parse(dilemmaInfo[3]));
        dilemmaButtonScript1.SetTurnCost(int.Parse(dilemmaInfo[4]));

        dilemmaButtonScript2.SetMoneyCost(int.Parse(dilemmaInfo[5]));
        dilemmaButtonScript2.SetTurnCost(int.Parse(dilemmaInfo[6]));

        //Set the dilemma image
        dilemmaImage.sprite = Resources.Load<Sprite>(dilemmaInfo[7].Trim());
    }
}
