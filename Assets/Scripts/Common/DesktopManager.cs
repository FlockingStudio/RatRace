using System.Collections;
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
    Credits,
    MetaGameEnd
}

public class DesktopManager : MonoBehaviour
{
    public static DesktopManager Instance { get; private set; }
    public GameObject GigWindowPrefab;
    public GameObject MailWindowPrefab;
    public GameObject MapWindowPrefab;
    public GameObject DilemmaWindowPrefab;
    public GameObject CreditsWindowPrefab;
    public GameObject MetaGameEndPrefab;
    public Button endButton;
    public Icon[] DesktopIcons;
    public bool Busy = false;
    private Canvas canvas;
    private Dictionary<WindowType, GameObject> windows = new Dictionary<WindowType, GameObject>();
    private Dictionary<WindowType, GameObject> prefabs = new Dictionary<WindowType, GameObject>();
    private readonly WaitForSeconds oneSecondWait = new(0.25f);



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
        prefabs[WindowType.MetaGameEnd] = MetaGameEndPrefab;
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
        StartCoroutine(EndSequenceCoroutine());
    }

    private IEnumerator EndSequenceCoroutine()
    {
        AudioManager.Instance.PlayBadEndMusic();

        foreach (WindowType type in windows.Keys)
        {
            switch (type)
            {
                case WindowType.Mail:
                    if (windows[type].activeSelf)
                        windows[type].GetComponent<Window>().MinimizeWindow();
                    yield return oneSecondWait;
                    MailList mailList = windows[type].GetComponentInChildren<MailList>();
                    mailList.ClearAllMail();
                    mailList.AddSpecialMail();
                    break;
                default:
                    if (windows[type] != null && windows[type].activeSelf)
                    {
                        windows[type].GetComponent<Window>().CloseWindow();
                        yield return oneSecondWait;
                    }
                    break;
            }
        }

        Busy = false;
        DesktopIcons[0].StartAnimation();
    }

    public void NextDaySequence()
    {
        StartCoroutine(NextDaySequenceCoroutine());
    }

    private IEnumerator NextDaySequenceCoroutine()
    {
        foreach (WindowType window in windows.Keys)
        {
            if (windows[window] != null)
            {
                switch (window)
                {
                    case WindowType.Mail:
                        if (windows[window].activeSelf)
                        {
                            windows[window].GetComponent<Window>().MinimizeWindow();
                            yield return oneSecondWait;
                        }
                        MailList mailList = windows[window].GetComponentInChildren<MailList>();
                        Player.Instance.ResetDailyStats();
                        GameManager.Instance.paidBillsToday = false;
                        mailList.ClearAllMail();
                        mailList.AddResultMail();
                        mailList.AddSubscriptionMail();
                        break;
                    default:
                        if (windows[window].activeSelf)
                        {
                            windows[window].GetComponent<Window>().CloseWindow();
                            yield return oneSecondWait;
                        }
                        break;
                }
            }
        }

        Busy = false;
        DesktopIcons[0].StartAnimation();
        AudioManager.Instance.PlayUpBeatMusic();

        Destroy(windows[WindowType.Map]);
    }

    public void AddReminderMail()
    {
        if (windows.ContainsKey(WindowType.Mail) && windows[WindowType.Mail] != null)
        {
            MailList mailList = windows[WindowType.Mail].GetComponentInChildren<MailList>();
            mailList.AddPaymentReminder();
        }
    }

    public void ShowEndButton()
    {
        if (endButton != null)
        {
            endButton.gameObject.SetActive(true);
            endButton.transform.SetAsLastSibling();
        }
    }

    // Legacy methods for backward compatibility
    public void OpenMail() => OpenWindow(WindowType.Mail);
    public void OpenGig() => OpenWindow(WindowType.Gig);
    public void OpenDilemma() => OpenWindow(WindowType.Dilemma);
    public void OpenMap() => OpenWindow(WindowType.Map);
    public void OpenCredits() => OpenWindow(WindowType.Credits);
    public void OpenMetagameEnd()
    {
        DesktopIcons[2].GetComponent<Button>().interactable = false;
        AudioManager.Instance.PlayAlert();
        OpenWindow(WindowType.MetaGameEnd);
    }
        
}
