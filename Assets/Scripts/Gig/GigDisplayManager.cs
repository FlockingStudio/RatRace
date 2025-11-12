using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static GameManager;

public class GigDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI gigText;

    public TextMeshProUGUI greaterThanText;

    public TextAsset easyGigCSV;
    public TextAsset hardGigCSV;

    public Button rollButton;
    public Button continueButton;


    private static CSVPool easyGigPool;
    private static CSVPool hardGigPool;

    private void Start()
    {
        // Create pool on first run
        if (easyGigPool == null)
        {
            easyGigPool = new CSVPool(easyGigCSV);
        }
        if (hardGigPool == null)
        {
            hardGigPool = new CSVPool(hardGigCSV);
        }

        LoadRandomGig();
        // Plays the correct track for the gig
        SoundManager.Instance.SwitchBackgroundMusic(Stage.gig);
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
            selectedGig = easyGigPool.GetRandom();
        }
        else //if (Player.Instance.GetEventDifficulty() == Difficulty.HARD)
        {
            selectedGig = hardGigPool.GetRandom();
        }

        // Parse the selected gig
        string[] gigInfo = selectedGig.Split(";");

        // Update UI elements
        gigText.SetText(gigInfo[0]);
        greaterThanText.SetText("Roll " + gigInfo[1] +" or more to win.");

        // Set required roll value on the button
        GigButton gigButtonScript = rollButton.GetComponent<GigButton>();
        gigButtonScript.setRequiredRoll(int.Parse(gigInfo[1]));
        gigButtonScript.setPayoutAmount(int.Parse(gigInfo[2]));
    }



    void OnDestroy()
    {
        if (Player.Instance.Turn <= 0)
        {
            GameManager.Instance.IsDayOver = true;
            GameManager.Instance.OpenGameOver();
        }
    }
}
