using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MailList : MonoBehaviour
{
    public GameObject MailItemPrefab;
    private float spacing = 135f; // Adjust this value to change the gap between items
    private float startOffset = 20f; // Offset for the first mail item (negative = higher up)
    private int mailItemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        string[] inputs = ParseCSVLine(GameManager.Instance.mailPool.GetFirst());
        AddMailItem(inputs[0], inputs[1], inputs[3], inputs[2] == "1");

        inputs = ParseCSVLine(GameManager.Instance.mailPool.GetFirst());
        AddMailItem(inputs[0], inputs[1], inputs[3], inputs[2] == "1");

        AddSubscriptionMails();
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

    public void AddMailItem(string from, string subject, string body, bool downloadable = false)
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

    public void RefreshMailList()
    {
        ClearAllMail();

        AddSubscriptionMails();
    }

    public void AddSubscriptionMails()
    {
        string[] inputs = ParseCSVLine(GameManager.Instance.mailPool.GetRandom());
        AddMailItem(inputs[0], inputs[1], inputs[3] + $"\nPayment of ${GameManager.Instance.targetMoney/2} will be processed within a week.");

        inputs = ParseCSVLine(GameManager.Instance.mailPool.GetRandom());
        AddMailItem(inputs[0], inputs[1], inputs[3] + $"\nPayment of ${GameManager.Instance.targetMoney/2} will be processed within a week.");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
