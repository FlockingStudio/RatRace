using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DDisplayManager : MonoBehaviour
{
    //dilemma csv order: body text, button text 1, button text 2, money effect of 1, money effect of 2, image name
    public TextMeshProUGUI dilemmaText;
    public Image dilemmaImage;
    public TextAsset dilemmaCSV;
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public Button button1;
    public Button button2;
    // Start is called before the first frame update
    void Start()
    {
        // Splits the string into sets of data
        string[] splitCSVData = dilemmaCSV.text.Split("\n");
        int textChoice = UnityEngine.Random.Range(0, splitCSVData.Length);
        // Splits each line into the text of the line, the value required by the dice, and the image
        string[] dilemmaInfo = splitCSVData[textChoice].Split(",");
        //Debug.Log(Info[2]);
        dilemmaText.SetText(dilemmaInfo[0]);
        choice1.SetText(dilemmaInfo[1]);
        choice2.SetText(dilemmaInfo[2]);
        // Gets the GigButton script attached to the roll button
        DilemmaButton dilemmaButtonScript1 = button1.GetComponent<DilemmaButton>();
        DilemmaButton dilemmaButtonScript2 = button2.GetComponent<DilemmaButton>();
        dilemmaButtonScript1.SetCost(int.Parse(dilemmaInfo[3]));
        dilemmaButtonScript2.SetCost(int.Parse(dilemmaInfo[4]));
        //Set the gig image
        dilemmaImage.sprite = Resources.Load<Sprite>(dilemmaInfo[5].Trim());
    }
}
