using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveDisplay : MonoBehaviour
{

    private TextMeshProUGUI objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        objectiveText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        objectiveText.text = "Objective: $" + GameManager.Instance.targetMoney.ToString();
    }
}
