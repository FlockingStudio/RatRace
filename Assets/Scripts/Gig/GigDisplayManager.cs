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
        DesktopManager.Instance.Busy = true;
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
        string targetText = $"Roll <color=#624391>{gigInfo[1]}</color> or more to win.";
        greaterThanText.SetText(targetText);//573C82little darker //624391 little lighter

        // Set required roll value on the button
        GigButton gigButtonScript = rollButton.GetComponent<GigButton>();
        gigButtonScript.setRequiredRoll(int.Parse(gigInfo[1]));
        gigButtonScript.setPayoutAmount(int.Parse(gigInfo[2]));
    }

    public void QuitButtonClick()
    {
        DesktopManager.Instance.Busy = false;

        if (Player.Instance.Turn < 1)
        {
            AudioManager.Instance.StopMusic();
            DesktopManager.Instance.OpenMetagameEnd();
            return;
        }
        
        AudioManager.Instance.PlayMapMusic();
    }
}
