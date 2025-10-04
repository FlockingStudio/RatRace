using TMPro;
using UnityEngine;

public class TurnDisplay : MonoBehaviour
{
   TextMeshProUGUI turnText;
    // Start is called before the first frame update
    void Start()
    {
        turnText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the text for the current turn
        turnText.SetText("Turn " + Player.Instance.Turn.ToString());
    }
}
