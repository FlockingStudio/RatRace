using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UserNameInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI maxCharacterText;

    void Start()
    {
        StartCoroutine(FocusInputField());
    }

    void Update()
    {
        // Adds the full text if the input is full
        if (inputField.text.Length == 7 && maxCharacterText.IsActive() == false)
        {
            maxCharacterText.gameObject.SetActive(true);
        }
        // Removes the full text if the input is not full
        else if (inputField.text.Length != 7 && maxCharacterText.IsActive() == true)
        {
            maxCharacterText.gameObject.SetActive(false);
        }
    }

    IEnumerator FocusInputField()
    {
        yield return null; // Wait one frame to ensure UI is initialized
        if (inputField != null)
        {
            inputField.ActivateInputField();
        }
    }
}
