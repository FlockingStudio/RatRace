using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailItem : MonoBehaviour
{
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
}
