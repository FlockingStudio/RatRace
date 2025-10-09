using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    private TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        moneyText.SetText(Player.Instance.GetMoney().ToString());
    }
}
