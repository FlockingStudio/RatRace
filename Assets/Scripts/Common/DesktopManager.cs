using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum WindowType
{
    Gig,
    Mail,
    Map,
    Dilemma,
    Credits
}

public class DesktopManager : MonoBehaviour
{
    public static DesktopManager Instance { get; private set; }
    public GameObject GigWindowPrefab;
    public GameObject MailWindowPrefab;
    public GameObject MapWindowPrefab;
    public GameObject DilemmaWindowPrefab;
    public GameObject CreditsWindowPrefab;
    public Icon[] DesktopIcons;
    private Canvas canvas;
    private Dictionary<WindowType, GameObject> windows = new Dictionary<WindowType, GameObject>();
    private Dictionary<WindowType, GameObject> prefabs = new Dictionary<WindowType, GameObject>();
    


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

        canvas = GetComponentInParent<Canvas>();

        // Initialize prefabs dictionary
        prefabs[WindowType.Gig] = GigWindowPrefab;
        prefabs[WindowType.Mail] = MailWindowPrefab;
        prefabs[WindowType.Map] = MapWindowPrefab;
        prefabs[WindowType.Dilemma] = DilemmaWindowPrefab;
        prefabs[WindowType.Credits] = CreditsWindowPrefab;
    }

    public void OpenWindow(WindowType type)
    {
        if (!windows.ContainsKey(type) || windows[type] == null)
        {
            windows[type] = Instantiate(prefabs[type], canvas.transform);
        }
        else
        {
            if (!windows[type].activeSelf)
            {

                windows[type].SetActive(true);
                windows[type].GetComponent<Window>().MaximizeWindow();
            }
        }
    }

    public void CloseWindow(WindowType type)
    {
        if (windows.ContainsKey(type) && windows[type] != null)
        {
            Destroy(windows[type]);
            windows[type] = null;
        }
    }

    public void ShowIcons()
    {
        foreach (Icon icon in DesktopIcons)
        {
            if (icon.gameObject.activeSelf == false)
            {
                icon.Show();
            }
        }
    }

    public void EndSequence()
    {
        foreach (GameObject window in windows.Values)
        {
            if (window != null)
            {
                MailList mailList = window.GetComponentInChildren<MailList>();
                if (mailList != null)
                {
                    window.SetActive(true);
                    mailList.ClearAllMail();
                    mailList.AddSpecialMail();
                }

                if (!window.activeSelf) continue;
                window.GetComponent<Window>().MinimizeWindow();
            }
        }

        DesktopIcons[0].StartAnimation();
        DesktopIcons[2].GetComponent<Button>().interactable = false;
    }

    public void NextDaySequence()
    {
        foreach (GameObject window in windows.Values)
        {
            if (window != null)
            {
                MailList mailList = window.GetComponentInChildren<MailList>();
                if (mailList != null)
                {
                    window.SetActive(true);
                    mailList.ClearAllMail();
                    mailList.AddResultMail();
                    mailList.AddSubscriptionMail();
                    mailList.AddPaymentReminder();
                }

                if (!window.activeSelf) continue;
                window.GetComponent<Window>().MinimizeWindow();
            }
        }

        DesktopIcons[0].StartAnimation();
        Player.Instance.ResetDailyStats();
        Destroy(windows[WindowType.Map]);
    }

    // Legacy methods for backward compatibility
    public void OpenMail() => OpenWindow(WindowType.Mail);
    public void OpenGig() => OpenWindow(WindowType.Gig);
    public void OpenDilemma() => OpenWindow(WindowType.Dilemma);
    public void OpenMap() => OpenWindow(WindowType.Map);
    public void OpenCredits() => OpenWindow(WindowType.Credits);

    public void CloseMail() => CloseWindow(WindowType.Mail);
    public void CloseGig() => CloseWindow(WindowType.Gig);
    public void CloseDilemma() => CloseWindow(WindowType.Dilemma);
    public void CloseMap() => CloseWindow(WindowType.Map);
    public void CloseCredits() => CloseWindow(WindowType.Credits);

}
