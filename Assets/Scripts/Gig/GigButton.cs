using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GigButton : MonoBehaviour
{
    public Image diceButton;
    public TextMeshProUGUI gigText;
    public TextMeshProUGUI rollButtonText;
    public Button quitButton;
    private string gigInformation;

    private int timesPressed = 0;
    private int requiredRoll = 0;

    private void Start()
    {
        gigInformation = gigText.text;
        if (Player.Instance.GetMoney() < 50)
        {
            gigText.text = "You do not have enough money to attempt this gig. Press quit gig to return to map";
            // Removes the option to roll
            GetComponent<Button>().gameObject.SetActive(false);
        }
    }
    public void ButtonLogic()
    {
        if (timesPressed == 0)
        {
            // Stops the player from interacting with either button
            GetComponent<Button>().interactable = false;
            quitButton.interactable = false;
            // Deduct cost of dice roll
            Player.Instance.SubtractMoney(50);

            // Start dice animation
            Dice diceScript = diceButton.GetComponent<Dice>();
            SoundManager.Instance.PlayDiceShake();
            diceScript.beginAnimation();

            // Schedule dice roll and scene transition
            Invoke("DiceRoll", 2.0f);
            Invoke("PlayRollSound", 2.0f);

            timesPressed = 1;
        }
        // Allows for the reroll option
        else if (timesPressed > 0)
        {
            // Stops the player from interacting with either button
            GetComponent<Button>().interactable = false;
            quitButton.interactable = false;
            // Resets the gig text to what it was before
            gigText.text = gigInformation;

            // Deduct cost of dice roll
            Player.Instance.SubtractMoney(50);

            // Start dice animation
            Dice diceScript = diceButton.GetComponent<Dice>();
            SoundManager.Instance.PlayDiceShake();
            diceScript.beginAnimation();

            // Schedule dice roll and scene transition
            Invoke("DiceRoll", 2.0f);
            Invoke("PlayRollSound", 2.0f);
            
        }

    }

    public void setRequiredRoll(int num)
    {
        requiredRoll = num;
    }

    private void DiceRoll()
    {

        // Roll the dice (1-6)
        int randomPick = UnityEngine.Random.Range(1, 7);

        // Complete the dice animation with the result
        Dice diceScript = diceButton.GetComponent<Dice>();
        diceScript.Complete(randomPick);

        // Award money if the roll exceeds the requirement
        if (randomPick > requiredRoll)
        {
            // Changes the gig text to show the player won
            gigText.text = "Success, you earned $150. Press Quit Gig to return to map.";
            Player.Instance.AddMoney(150);
            // Removes the option to reroll and allows the player to quit
            GetComponent<Button>().gameObject.SetActive(false);
            quitButton.interactable = true;
        }
        // Award money if the roll exceeds the requirement
        else
        {
            // Checks if the player has enough money to reroll
            if (Player.Instance.GetMoney() >= 50)
            {
                // Changes the gig text to show the player won
                gigText.text = "You failed. Roll again or press Quit Gig to go to map.";
                // Allows the player to interact with both buttons
                GetComponent<Button>().interactable = true;
                quitButton.interactable = true;
            }
            else
            {
                // Changes the gig text to show the player lost and can't reroll
                gigText.text = "You failed and can't afford to roll again. Press Quit Gig to return to the map";
                // Only allows the player to interact with the quit button
                GetComponent<Button>().gameObject.SetActive(false);
                quitButton.interactable = true;
            }

        }
    }

    private void PlayRollSound()
    {
        SoundManager.Instance.PlayDiceRoll();
    }
}
