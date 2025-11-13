using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailList : MonoBehaviour
{
    public TextAsset csvFile;
    public GameObject MailItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CSVPool pool = new CSVPool(csvFile);
        List<string> inputs = pool.GetAll();
        float yOffset = 0f;
        float spacing = 140f; // Adjust this value to change the gap between items

        foreach (string input in inputs)
        {
            string[] parts = input.Split(',');
            string from = parts[0];
            string subject = parts[1];
            string downloadStr = parts.Length > 2 ? parts[2] : "0";
            bool download = downloadStr == "1";

            GameObject mailItem = Instantiate(MailItemPrefab, transform);
            RectTransform rectTransform = mailItem.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0, yOffset);

            mailItem.GetComponent<MailItem>().SetTexts(from, subject);
            mailItem.GetComponent<MailItem>().downloadable = download;
            yOffset -= spacing;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
