using System;
using System.Collections;
using System.Collections.Generic;
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
            // Makes the dice visible
            diceImage.enabled = true;
            timesPressed += 1;
        }
        else if (timesPressed == 1)
        {
            // Charges for a dice roll
            Player.Instance.Money -= 50;
            Invoke("DiceRoll", 2.0f);
            timesPressed += 1;
        }
    }

    // Makes a function to roll the dice so that it can be called with invoke
    void DiceRoll()
    {
        Debug.Log("Dice roll function");
        //Stops the dice if it is pressed again
        Dice d = diceImage.GetComponent<Dice>();
        int choice = d.Complete();
        if (choice > 3)
        {
            Player.Instance.Money += 200;
        }
    }
}
