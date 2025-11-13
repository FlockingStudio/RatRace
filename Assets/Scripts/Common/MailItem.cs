using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailItem : MonoBehaviour
{
    public bool downloadable = false;
    public GameObject MailWindowPrefab;
    private GameObject mailWindowInstance;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTexts(string from, string subject)
    {
        TextMeshProUGUI fromInput = transform.Find("FromInput").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subjectInput = transform.Find("SubjectInput").GetComponent<TextMeshProUGUI>();

        fromInput.text = from;
        subjectInput.text = subject;
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
        bodyInput.text = "yo";

        if (downloadable)
        {
            ShowDownloadButton();
        }
    }

    public void ShowDownloadButton()
    {
        Transform downloadButton = mailWindowInstance.transform.Find("DownloadButton");
        if (downloadButton != null)
        {
            downloadButton.gameObject.SetActive(true);
        }
    }
}
