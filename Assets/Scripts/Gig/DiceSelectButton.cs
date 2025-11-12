using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiceSelectButton : MonoBehaviour
{
    public int diceType;
    public TextMeshProUGUI buttonText;
    public Dice dice;
    public Button rollButton;
    private int dicePrice;
    // Start is called before the first frame update
    void Start()
    {
        // Sets the display for what the button should like like at start
        List<int> dicePrices = Player.Instance.GetDicePrices();
        Debug.Log(dicePrices);
        if (diceType == 6)
        {
            dicePrice = dicePrices[0];
        }
        else if (diceType == 8)
        {
            dicePrice = dicePrices[1];
        }
        else if (diceType == 12)
        {
            dicePrice = dicePrices[2];
        }
        buttonText.text = "1-" + diceType.ToString() + " Dice, $" + dicePrice.ToString();
        // Prevents the button from being clicked if the played doesn't have enough money
        if (Player.Instance.GetMoney() < dicePrice)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    public void DiceSelectButtonClick()
    {
        dice.GameObject().SetActive(true);
        dice.SetDiceType(diceType);
        rollButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Roll $" + dicePrice.ToString());
        rollButton.GameObject().SetActive(true);
        // Allows the gig button to know the dice type and price
        GigButton rollScript = rollButton.GetComponent<GigButton>();
        rollScript.SetDicePrice(dicePrice);
        rollScript.SetDiceType(diceType);
    }
}
