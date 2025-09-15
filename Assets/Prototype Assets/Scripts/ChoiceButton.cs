using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by craig on sept 14
//this is completely fungled as an implementation and is only for the sake of the prototype. 
//the real solution needs to be giving each button its own script so that each button can have a different effect. 
//i'm just doing it this way now because both buttons are just gonna destroy the UI
public class ChoiceButton : MonoBehaviour
{
    public GameObject DilemmaHost;

    public void OnClickAction()
    {
        Destroy(DilemmaHost);
    }
}
