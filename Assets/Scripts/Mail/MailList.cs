using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MailList : MonoBehaviour
{
    public GameObject MailItemPrefab;
    private float spacing = 135f; // Adjust this value to change the gap between items
    private float startOffset = 15f; // Offset for the first mail item (negative = higher up)
    private int mailItemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        string[] inputs = ParseCSVLine(GameManager.Instance.specialMailPool.GetFirst());
        AddMailItem(inputs[0], inputs[1], inputs[2]);

        AddSubscriptionMail();
        AddPaymentReminder();

        inputs = ParseCSVLine(GameManager.Instance.specialMailPool.GetFirst());
        AddMailItem(inputs[0], inputs[1], inputs[2], true);
    }

    private string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        string pattern = @",(?=(?:[^""]*""[^""]*"")*[^""]*$)";
        string[] fields = Regex.Split(line, pattern);

        foreach (string field in fields)
        {
            // Remove leading/trailing whitespace and quotes
            string cleaned = field.Trim();
            if (cleaned.StartsWith("\"") && cleaned.EndsWith("\""))
            {
                cleaned = cleaned.Substring(1, cleaned.Length - 2);
            }
            // Replace \n escape sequences with actual newlines
            cleaned = cleaned.Replace("\\n", "\n");
            result.Add(cleaned);
        }

        return result.ToArray();
    }

    public void AddMailItem(string from, string subject, string body, bool downloadable = false, bool endButton = false, bool payBillsButton = false)
    {
        float yOffset = startOffset + (-mailItemCount * spacing);

        GameObject mailItem = Instantiate(MailItemPrefab, transform);
        RectTransform rectTransform = mailItem.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector2(0, yOffset);

        mailItem.GetComponent<MailItem>().SetTexts(from, subject, body);
        mailItem.GetComponent<MailItem>().downloadable = downloadable;
        mailItem.GetComponent<MailItem>().endButton = endButton;
        mailItem.GetComponent<MailItem>().payBillsButton = payBillsButton;

        mailItemCount++;
    }

    public void ClearAllMail()
    {
        // Destroy all child mail items
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Reset the counter
        mailItemCount = 0;
    }

    public void AddSubscriptionMail()
    {
        string[] inputs = ParseCSVLine(GameManager.Instance.subscriptionMailPool.GetRandom());
        AddMailItem(inputs[0], inputs[1], inputs[2]);
    }

    public void AddPaymentReminder()
    {
        string from = "BankPaymore";
        string subject = "Upcoming Payment Reminder";
        string body = $"Dear Customer,\n\nThis is a reminder that your scheduled payment of <color=#800000>${50 + 25 * (Player.Instance.Day - 1)}</color> is due by the <color=#800000>end of today.</color> Please ensure that sufficient funds are available in your account to avoid any significant penalties.\n\nThank you for choosing BankPaymore for your financial needs.";
        AddMailItem(from, subject, body);
    }

    public void AddResultMail()
    {
        string from = "BankPaymore";
        string subject = "Payment Required";
        string body = $"Dear Customer,\n\nYour scheduled payment of <color=#800000>${GameManager.Instance.targetMoney}</color> was not received by the due date. Please make this payment <color=#800000>immediately</color> to avoid further penalties.\n\nSincerely,\nBankPaymore Collections Department";
        AddMailItem(from, subject, body, false, false, true);
    }

    public void AddSpecialMail()
    {
        string[] inputs = ParseCSVLine(GameManager.Instance.specialMailPool.GetRandom());
        AddMailItem(inputs[0], inputs[1], inputs[2], false, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
