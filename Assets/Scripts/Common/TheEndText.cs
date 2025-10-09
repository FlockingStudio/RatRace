using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheEndText : MonoBehaviour
{
    private TextMeshProUGUI endText;

    private void Start()
    {
        endText = GetComponent<TextMeshProUGUI>();
        if (Player.Instance.GetMoney() < 350)
        {
            endText.SetText("You died");
            // disable the button
            FindFirstObjectByType<UnityEngine.UI.Button>().interactable = false;
        } else
        {
            endText.SetText("You made it! Click to continue!");
        }
    }

    private void Update()
    {

    }

    public void OpenMap() {
        GameManager.Instance.MoveToNextDay();
    }
}
