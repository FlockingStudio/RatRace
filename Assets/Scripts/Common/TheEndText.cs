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
        } else
        {
            endText.SetText("You made it!");
        }
    }

    private void Update()
    {

    }
}
