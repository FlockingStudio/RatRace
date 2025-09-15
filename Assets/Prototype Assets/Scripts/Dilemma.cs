using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//made by craig on sept 14
//what needs to happen for this to actually work is that a central script needs to be made to handle player resources and have a few different methods
//that handle more advanced effects we want to give to dilemmas, and then have that script referenced in here so that the dilemmas can actually do stuff mechanically.
public class Dilemma : MonoBehaviour
{
    public TextMeshProUGUI TMBodyText;
    public Button ButtonChoice1;
    public Button ButtonChoice2;
    public string BodyText;
    public string Choice1;
    public string Choice2;

    void Awake()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        TMBodyText.text = BodyText;
        TextMeshProUGUI tmp1 = ButtonChoice1.GetComponentInChildren<TextMeshProUGUI>();
        tmp1.text = Choice1;
        TextMeshProUGUI tmp2 = ButtonChoice2.GetComponentInChildren<TextMeshProUGUI>();
        tmp2.text = Choice2;
    }
}
