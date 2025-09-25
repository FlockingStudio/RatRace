using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GigButton : MonoBehaviour
{
    private int timesPressed = 0;
    public UnityEngine.UI.Image textBox;
    public TextMeshProUGUI gigText;
    public UnityEngine.UI.Image diceImage;
    // Call the method to run the logic of the button
    public void ButtonLogic()
    {
        // If first time pressing the button remove the text display
        if (timesPressed == 0)
        {
            // Makes the text invisible
            gigText.alpha = 0;
            textBox.enabled = false;
            Player.money -= 10;
            // Makes the dice visible
            diceImage.enabled = true;
        }
        else if (timesPressed == 1)
        {
            //Player.money -= 20;
        }
    }
}
