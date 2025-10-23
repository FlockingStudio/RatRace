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
        int remainingTurns = Player.Instance.Turn;
        turnText.SetText("Turns left: " + remainingTurns.ToString());
        turnText.color = Player.Instance.Turn <= 1 ? new Color(0.8f, 0f, 0f) : Color.white;

    }
}
