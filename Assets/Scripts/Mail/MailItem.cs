using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailItem : MonoBehaviour
{
    public bool downloadable = false;
    public bool endButton = false;
    public bool payBillsButton = false;
    public GameObject MailWindowPrefab;
    private GameObject mailWindowInstance;
    private string bodyText;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTexts(string from, string subject, string body)
    {
        TextMeshProUGUI fromInput = transform.Find("FromInput").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subjectInput = transform.Find("SubjectInput").GetComponent<TextMeshProUGUI>();

        fromInput.text = from;
        subjectInput.text = subject;
        bodyText = body;
    }

    public void OpenMailWindow()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        mailWindowInstance = Instantiate(MailWindowPrefab, canvas.transform);
        // find 3 text components and set their text to the values from this mail item
        TextMeshProUGUI fromInput = mailWindowInstance.transform.Find("FromInput").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subjectInput = mailWindowInstance.transform.Find("SubjectInput").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bodyInput = mailWindowInstance.transform.Find("BodyInput").GetComponent<TextMeshProUGUI>();
        fromInput.text = transform.Find("FromInput").GetComponent<TextMeshProUGUI>().text;
        subjectInput.text = transform.Find("SubjectInput").GetComponent<TextMeshProUGUI>().text;
        bodyInput.text = bodyText;

        // Check if all buttons are hidden - if so, expand body text area
        bool anyButtonActive = downloadable || endButton || payBillsButton;
        if (!anyButtonActive)
        {
            ExpandBodyInput(bodyInput);
        }

        if (downloadable)
        {
            ShowDownloadButton();
        }

        if (endButton)
        {
            ShowEndButton();
        }

        if (payBillsButton)
        {
            ShowPayBillsButton(!GameManager.Instance.paidBillsToday);
        }
    }

    private void ShowDownloadButton()
    {
        Transform downloadButton = mailWindowInstance.transform.Find("DownloadButton");
        if (downloadButton != null)
        {
            downloadButton.gameObject.SetActive(true);
            if (DesktopManager.Instance.DesktopIcons[2].gameObject.activeSelf)
            {
                downloadButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void ShowEndButton()
    {
        Transform endButton = mailWindowInstance.transform.Find("EndButton");
        if (endButton != null)
        {
            endButton.gameObject.SetActive(true);
        }
    }

    public void ShowPayBillsButton(bool interactable)
    {
        Transform payBillsButton = mailWindowInstance.transform.Find("PayBillsButton");
        if (payBillsButton != null)
        {
            payBillsButton.gameObject.SetActive(true);
            payBillsButton.GetComponent<Button>().interactable = interactable;
        }
    }

    private void ExpandBodyInput(TextMeshProUGUI bodyInput)
    {
        RectTransform bodyRect = bodyInput.GetComponent<RectTransform>();
        if (bodyRect != null)
        {
            // Increase height by 150 units downward (buttons take ~150-200 vertical space)
            Vector2 currentSize = bodyRect.sizeDelta;
            bodyRect.sizeDelta = new Vector2(currentSize.x, currentSize.y + 120f);

            // Move down by half the added height to expand downward
            Vector2 currentPos = bodyRect.anchoredPosition;
            bodyRect.anchoredPosition = new Vector2(currentPos.x, currentPos.y - 55f);
        }
    }
}
