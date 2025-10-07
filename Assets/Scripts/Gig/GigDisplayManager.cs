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

    private void Start()
    {
        LoadRandomGig();
    }

    private void LoadRandomGig()
    {
        // Parse CSV data
        string[] splitCSVData = gigCSV.text.Split("\n");
        int textChoice = Random.Range(0, splitCSVData.Length);
        string[] gigInfo = splitCSVData[textChoice].Split(",");

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
