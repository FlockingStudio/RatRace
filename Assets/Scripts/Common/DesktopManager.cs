using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    public static DesktopManager Instance { get; private set; }
    public GameObject GigWindow;
    public GameObject MailWindow;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

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
