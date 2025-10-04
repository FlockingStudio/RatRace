using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GigButton : MonoBehaviour
{
    // Call the method to run the logic of the button
    private int timesPressed = 0;
    private int requiredRoll = 0;
    public Image diceButton;
    public AudioSource shakeSound;
    public AudioSource rollSound;
    public void ButtonLogic()
    {
        if (timesPressed == 0)
        {
            // Charges for a dice roll
            Player.Instance.Money -= 50;
            // Starts the diceroll animation
            Dice diceScript = diceButton.GetComponent<Dice>();
            // Plays the shaking sound effect
            shakeSound.Play();
            diceScript.beginAnimation();
            Invoke("DiceRoll", 2.0f);
            timesPressed = 1;
            // Plays the throwing sound effect
            Invoke("PlayRollSound", 2.0f);
            // Waits a few seconds to open the map scene
            Invoke("SwitchToMap", 3.5f);
        }
    }

    // Makes a function to roll the dice so that it can be called with invoke
    void DiceRoll()
    {
        Debug.Log("Dice roll function required roll " + requiredRoll);
        // Rolls the dice
        int randomPick = UnityEngine.Random.Range(1, 7);
        // Ends the diceroll animation
        Dice diceScript = diceButton.GetComponent<Dice>();
        diceScript.Complete(randomPick);
        if (randomPick > requiredRoll)
        {
            Player.Instance.Money += 200;
        }
    }

    // Sets the amount the player has to achieve for the dice
    public void setRequiredRoll(int num)
    {
        requiredRoll = num;
    }

    // Switch to map scene
    void SwitchToMap()
    {
        GameManager.Instance.OpenMap();
    }

    // Plays the roll sound
    void PlayRollSound()
    {
        rollSound.Play();
    }
}
