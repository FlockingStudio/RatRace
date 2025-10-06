using TMPro;
using UnityEngine;

public class DayDisplay : MonoBehaviour
{
    private TextMeshProUGUI dayText;

    private void Start()
    {
        dayText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        dayText.SetText("Day " + Player.Instance.Day.ToString());
    }
}
