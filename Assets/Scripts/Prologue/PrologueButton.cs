using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PrologueButton : MonoBehaviour
{
    int counter = 3;//change this to 0 and un-comment the bottom text if we want to scroll
    public string[] dialogueSegments;
    public TextMeshProUGUI modText;
    public void PressButton()
    {
        if(counter < dialogueSegments.Length)
        {
            modText.text = dialogueSegments[counter];
            counter++;
        }
        else
        {
            GameManager.Instance.OpenMap();
            // disable button to prevent spamming
            GetComponent<Button>().enabled = false;
        }
    }

    void Start()
    {
        //modText.text = dialogueSegments[counter];
        counter++;
    }
}
