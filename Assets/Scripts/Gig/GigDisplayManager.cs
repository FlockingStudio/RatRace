using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GigDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI gigText;
    public TextMeshProUGUI greaterThanText;
    public Image gigImage;
    public TextAsset gigCSV;
    public Button rollButton;
    // Start is called before the first frame update
    void Start()
    {
        // Splits the string into sets of data
        string[] splitCSVData = gigCSV.text.Split("\n");
        int textChoice = UnityEngine.Random.Range(0, splitCSVData.Length);
        // Splits each line into the text of the line, the value required by the dice, and the image
        string[] gigInfo = splitCSVData[textChoice].Split(",");
        Debug.Log(gigInfo[2]);
        gigText.SetText(gigInfo[0]);
        greaterThanText.SetText("Roll greater than >" + gigInfo[1]);
        // Gets the GigButton script attached to the roll button
        GigButton gigButtonScript = rollButton.GetComponent<GigButton>();
        gigButtonScript.setRequiredRoll(int.Parse(gigInfo[1]));
        //Set the gig image
        gigImage.sprite = Resources.Load<Sprite>(gigInfo[2].Trim());
    }
}
