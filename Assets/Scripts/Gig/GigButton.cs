using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GigButton : MonoBehaviour
{
    public Image diceImage;
    public TextMeshProUGUI gigText;
    public TextMeshProUGUI rollButtonText;
    public Button quitButton;
    public Button sixButton;
    public Button eightButton;
    public Button twelveButton;
    private string gigInformation;

    private int timesPressed = 0;
    private int requiredRoll = 0;
    private int payoutAmout = 0;
    private int diceType;
    private int dicePrice;

    private void Start()
    {
        gigInformation = gigText.text;
        if (Player.Instance.GetMoney() < Player.Instance.GetDicePrices().Min())
        {
            //gigText.text = "You do not have enough money to attempt this gig. Press quit gig to return to map";
            // Removes the option to roll
            GetComponent<Button>().gameObject.SetActive(false);
        }
    }
    public void ButtonLogic()
    {
        // Stops the player from interacting with either button
        GetComponent<Button>().interactable = false;
        quitButton.interactable = false;
        sixButton.interactable = false;
        eightButton.interactable = false;
        twelveButton.interactable = false;
        // Deduct cost of dice roll
        Player.Instance.SubtractMoney(dicePrice);

        // Start dice animation
        Dice diceScript = diceImage.GetComponent<Dice>();
        SoundManager.Instance.PlayDiceShake();
        diceScript.beginAnimation();

        // Schedule dice roll and scene transition
        Invoke("DiceRoll", 2.0f);
        Invoke("PlayRollSound", 2.0f);

        timesPressed = 1;

    }

    public void setRequiredRoll(int num)
    {
        requiredRoll = num;
    }

    public void setPayoutAmount(int num)
    {
        payoutAmout = num;
    }

    private void DiceRoll()
    {

        // Roll the dice from 1 to whatever thwe max of the dice is
        int randomPick = UnityEngine.Random.Range(1, diceType + 1);

        // Complete the dice animation with the result
        Dice diceScript = diceImage.GetComponent<Dice>();
        diceScript.Complete(randomPick);

        // Award money if the roll exceeds the requirement
        if (randomPick > requiredRoll)
        {
            // Changes the gig text to show the player won
            //gigText.text = "Success, you earned $" + payoutAmout.ToString() + ". Press Quit Gig to return to map.";
            Player.Instance.AddMoney(payoutAmout);
            // Removes the option to reroll and allows the player to quit
            GetComponent<Button>().gameObject.SetActive(false);
            quitButton.interactable = true;
        }
        // Award money if the roll exceeds the requirement
        else
        {
            // Checks if the player has enough money to reroll
            if (Player.Instance.GetMoney() >= Player.Instance.GetDicePrices().Min())
            {
                // Changes the gig text to show the player won
                //gigText.text = "You failed. Roll again or press Quit Gig to go to map.";
                // Allows the player to interact with both buttons
                GetComponent<Button>().interactable = true;
                sixButton.interactable = true;
                eightButton.interactable = true;
                twelveButton.interactable = true;
                quitButton.interactable = true;
            }
            else
            {
                // Changes the gig text to show the player lost and can't reroll
                //gigText.text = "You failed and can't afford to roll again. Press Quit Gig to return to the map";
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

    public void SetDiceType(int diceKind)
    {
        diceType = diceKind;
    }
    public void SetDicePrice(int price)
    {
        dicePrice = price;
    }
}
