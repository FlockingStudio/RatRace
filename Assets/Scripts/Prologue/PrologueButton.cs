using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PrologueButton : MonoBehaviour
{
    int counter = 0;
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
        }
    }

    void Start()
    {
        modText.text = dialogueSegments[counter];
        counter++;
    }
}
