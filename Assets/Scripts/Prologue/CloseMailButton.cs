using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMailButton : MonoBehaviour
{
    public Window window;
    public new int tag;
    
    public void Close()
    {
        window.CloseWindow();
        if (tag == 1)
        {
            GameObject obj = GameObject.Find("landlordbutton");
            obj.GetComponent<MailUI>().mailOpen = false;
        }
        else if (tag == 2)
        {
            GameObject obj = GameObject.Find("gamebutton");
            obj.GetComponent<MailUI>().mailOpen = false;
        }
    }
}
