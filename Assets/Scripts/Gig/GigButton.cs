using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GigButton : MonoBehaviour
{
    public Image diceButton;
    public TextMeshProUGUI gigText;
    public Button completeButton;
    public TextMeshProUGUI rollButtonText;
    private string gigInformation;
    private bool diceRollComplete;

    private int timesPressed = 0;
    private int requiredRoll = 0;

    private void Start()
    {
        gigInformation = gigText.text;
        diceRollComplete = false;
    }
    public void ButtonLogic()
    {
        if (timesPressed == 0)
        {
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
        else if (timesPressed > 0 && diceRollComplete)
        {
            // Stops the user from spamming the roll option
            diceRollComplete = false;
            // Removes the complete button
            completeButton.gameObject.SetActive(false);
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
        Debug.Log("Dice roll function required roll " + requiredRoll);

        // Roll the dice (1-6)
        int randomPick = Random.Range(1, 7);

        // Complete the dice animation with the result
        Dice diceScript = diceButton.GetComponent<Dice>();
        diceScript.Complete(randomPick);

        // Award money if the roll exceeds the requirement
        if (randomPick > requiredRoll)
        {
            // Changes the gig text to show the player won
            gigText.text = "Success, you earned $150. Press continue to return to map.";
            Player.Instance.AddMoney(150);
            diceRollComplete = true;
            // Removes the option to reroll
            GetComponent<Button>().gameObject.SetActive(false);
            // Adds the complete button
            completeButton.gameObject.SetActive(true);
        }
        // Award money if the roll exceeds the requirement
        else
        {
            // Checks if the player has enough money to reroll
            if (Player.Instance.GetMoney() >= 50)
            {
                // Changes the gig text to show the player won
                gigText.text = "You failed. Reroll or press continue to go to map.";
                diceRollComplete = true;
                // Adds the complete button
                completeButton.gameObject.SetActive(true);
                // Changes the button text to say reroll
                rollButtonText.text = "Reroll $50";
            }
            else
            {
                // Changes the gig text to show the player lost and can't reroll
                gigText.text = "You failed and can't afford to reroll. Press continue to return to the map";
                diceRollComplete = true;
                // Removes the option to reroll
                GetComponent<Button>().gameObject.SetActive(false);
                // Adds the complete button
                completeButton.gameObject.SetActive(true);
            }

        }
    }

    private void PlayRollSound()
    {
        SoundManager.Instance.PlayDiceRoll();
    }
}
