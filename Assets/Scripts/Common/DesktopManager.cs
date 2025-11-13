using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    public GameObject GigWindow;
    public GameObject MailWindow;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OpenMail()
    {
        Instantiate(MailWindow, canvas.transform);
    }

    public void OpenGig()
    {
        Instantiate(GigWindow, canvas.transform);
    }
}
