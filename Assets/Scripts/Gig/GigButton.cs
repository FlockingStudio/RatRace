using System;
using System.Collections.Generic;
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

    private int requiredRoll = 0;
    private int payoutAmout = 0;
    private int diceType;
    private int dicePrice;

    private void Start()
    {
        
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
        diceScript.beginAnimation();

        // Schedule dice roll and scene transition
        Invoke("DiceRoll", 2.0f);
        Invoke("PlayRollSound", 2.0f);
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
        if (randomPick >= requiredRoll)
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
                MakeDiceButtonsInteractable();
                quitButton.interactable = true;
            }
            else
            {
                // Changes the gig text to show the player lost and can't reroll
                gigText.text = "You can't afford to roll again. Please Leave the casino.";
                // Only allows the player to interact with the quit button
                GetComponent<Button>().gameObject.SetActive(false);
                quitButton.interactable = true;
            }

        }
    }

    private void PlayRollSound()
    {
    }

    public void SetDiceType(int diceKind)
    {
        diceType = diceKind;
    }
    public void SetDicePrice(int price)
    {
        dicePrice = price;
    }

    // Makes only dice buttons that the player can afford interactable
    public void MakeDiceButtonsInteractable()
    {
        List<int> dicePrices = Player.Instance.GetDicePrices();
        int playerMoney = Player.Instance.GetMoney();
        if (dicePrices[0] <= playerMoney)
        {
            sixButton.interactable = true;
        }
        if (dicePrices[1] <= playerMoney)
        {
            eightButton.interactable = true;
        }
        if (dicePrices[2] <= playerMoney)
        {
            twelveButton.interactable = true;
        }
    }
}
