using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserNameInput : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        StartCoroutine(FocusInputField());
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
