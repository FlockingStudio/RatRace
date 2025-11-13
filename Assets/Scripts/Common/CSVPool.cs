using System.Collections.Generic;
using UnityEngine;

public class CSVPool
{
    private List<string> availableItems;
    private TextAsset csvFile;

    public CSVPool(TextAsset csv)
    {
        csvFile = csv;
        LoadAll();
    }

    private void LoadAll()
    {
        availableItems = new List<string>();
        string[] splitCSVData = csvFile.text.Split("\n");

        foreach (string item in splitCSVData)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                availableItems.Add(item);
            }
        }
    }

    public string GetRandom()
    {
        // If no items left, reload all items
        if (availableItems.Count == 0)
        {
            LoadAll();
        }

        // Get random item
        int randomIndex = Random.Range(0, availableItems.Count);
        string selectedItem = availableItems[randomIndex];

        // Remove from available list
        availableItems.RemoveAt(randomIndex);

        return selectedItem;
    }

    public List<string> GetAll()
    {
        LoadAll();
        return availableItems;
    }
}
