using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MailBuilder : MonoBehaviour
{
    public int mailIndex = 0;
    public bool IsEnd = false;

    private TextMeshProUGUI toText;
    private TextMeshProUGUI subjectText;
    private TextMeshProUGUI bodyText;
    private TextMeshProUGUI fromText;
    private TextMeshProUGUI buttonText;
    private Button button;

    void Start()
    {
        // Find child TextMeshPro components
        toText = transform.Find("To").GetComponent<TextMeshProUGUI>();
        subjectText = transform.Find("Subject").GetComponent<TextMeshProUGUI>();
        bodyText = transform.Find("Body").GetComponent<TextMeshProUGUI>();
        fromText = transform.Find("From").GetComponent<TextMeshProUGUI>();
        button = transform.Find("Button").GetComponent<Button>();
        buttonText = transform.Find("Button").Find("ButtonText").GetComponent<TextMeshProUGUI>();

        if (GameManager.Instance.IsDayOver)
        {
            if (Player.Instance.GetMoney() >= 400)
            {
                mailIndex = 1;
            }
            else
            {
                mailIndex = 2;
            }
        }

        LoadMailData();
    }

    void LoadMailData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Mail");
        if (csvFile == null)
        {
            Debug.LogError("Mail.csv not found in Resources folder");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        if (mailIndex >= lines.Length || mailIndex < 0)
        {
            Debug.LogError("Mail index out of range");
            return;
        }

        string line = lines[mailIndex];
        string[] columns = ParseCSVLine(line);

        if (columns.Length >= 5)
        {
            toText.text = columns[0];
            subjectText.text = columns[1];
            bodyText.text = columns[2].Replace("\\n", "\n");
            fromText.text = columns[3];
            buttonText.text = columns[4];
            if (mailIndex == 0)
            {
                button.onClick.AddListener(GameManager.Instance.OpenMap);
            } else
            {
                button.onClick.AddListener(GameManager.Instance.RestartGame);
            }
        }
    }

    string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }

        result.Add(current);
        return result.ToArray();
    }
}
