using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayDisplay : MonoBehaviour
{
    TextMeshProUGUI dayText;
    // Start is called before the first frame update
    void Start()
    {
        dayText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the text for the amount of money the player has
        dayText.SetText("Day " + GameManager.Instance.Day.ToString());
    }

}
