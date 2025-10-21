using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GigDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI gigText;

    public TextMeshProUGUI greaterThanText;

    public Image gigImage;

    public TextAsset gigCSV;

    public Button rollButton;
    public Button continueButton;

    private static CSVPool gigPool;

    private void Start()
    {
        // Create pool on first run
        if (gigPool == null)
        {
            gigPool = new CSVPool(gigCSV);
        }

        LoadRandomGig();
        // Plays the correct track for the gig
        SoundManager.Instance.playTrackTwo();
    }

    private void LoadRandomGig()
    {
        // Get random gig from pool
        string selectedGig = gigPool.GetRandom();

        // Parse the selected gig
        string[] gigInfo = selectedGig.Split(",");

        // Update UI elements
        gigText.SetText(gigInfo[0]);
        greaterThanText.SetText("Roll greater than >" + gigInfo[1]);

        // Set required roll value on the button
        GigButton gigButtonScript = rollButton.GetComponent<GigButton>();
        gigButtonScript.setRequiredRoll(int.Parse(gigInfo[1]));

        // Load and set the gig image from Resources
        gigImage.sprite = Resources.Load<Sprite>(gigInfo[2].Trim());

        Debug.Log("Loaded gig with image: " + gigInfo[2]);
    }
}
