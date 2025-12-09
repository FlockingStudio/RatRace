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
        //moneyText.color = Player.Instance.GetMoney() >= 400 ? new Color(0f, 0.5f, 0f) : Color.white;
    }
}
