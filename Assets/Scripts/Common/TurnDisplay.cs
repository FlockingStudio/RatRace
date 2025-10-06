using TMPro;
using UnityEngine;

public class TurnDisplay : MonoBehaviour
{
    private TextMeshProUGUI turnText;

    private void Start()
    {
        turnText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        turnText.SetText("Turns left: " + Player.Instance.Turn.ToString());
    }
}
