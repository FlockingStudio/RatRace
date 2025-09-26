using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        // Displays the text for the amount of money the player has
        moneyText.SetText(Player.Instance.Money.ToString());
    }
    
}
