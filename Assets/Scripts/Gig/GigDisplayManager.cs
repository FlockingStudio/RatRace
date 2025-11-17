using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GigDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI gigText;

    public TextMeshProUGUI greaterThanText;

    public Button rollButton;
    public Button continueButton;


    private void Start()
    {
        LoadRandomGig();
        AudioManager.Instance.PlayGigMusic();
        // Plays the correct track for the gig
        if (Player.Instance.GetMoney() < Player.Instance.GetDicePrices().Min())
        {
            gigText.text = "You can't afford to roll. Please Leave the casino.";
            // Removes the option to roll
            rollButton.gameObject.SetActive(false);
        }
    }

    private void LoadRandomGig()
    {
        // Get random gig from pool
        string selectedGig;
        if (Player.Instance.GetEventDifficulty() == Difficulty.EASY)
        {
            selectedGig = GameManager.Instance.easyGigPool.GetRandom();
        }
        else //if (Player.Instance.GetEventDifficulty() == Difficulty.HARD)
        {
            selectedGig = GameManager.Instance.hardGigPool.GetRandom();
        }

        // Parse the selected gig
        string[] gigInfo = selectedGig.Split(",");

        // Update UI elements
        gigText.SetText(gigInfo[0]);
        greaterThanText.SetText("Roll " + gigInfo[1] +" or more to win.");

        // Set required roll value on the button
        GigButton gigButtonScript = rollButton.GetComponent<GigButton>();
        gigButtonScript.setRequiredRoll(int.Parse(gigInfo[1]));
        gigButtonScript.setPayoutAmount(int.Parse(gigInfo[2]));
    }

    public void QuitButtonClick()
    {
        AudioManager.Instance.StopMusic();
        if (Player.Instance.Turn < 1)
        {
           DesktopManager.Instance.OpenMetagameEnd();
        }
    }
}
