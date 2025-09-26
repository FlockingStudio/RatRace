using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour
{
    public Image[] tabs;
    // Start is called before the first frame update
    void Start()
    {
        int currentStage = (int)Player.Instance.CurrentStage;

        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == currentStage)
            {
                tabs[i].color = Color.grey;
            }
            else if (i > currentStage)
            {
                tabs[i].color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
