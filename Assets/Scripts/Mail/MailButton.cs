using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MailButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDownloadButtonClicked()
    {
        AudioManager.Instance.PlayBrowserClick();
        DesktopManager.Instance.ShowIcons();
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void OnEndButtonClicked()
    {
        GameManager.Instance.OpenLeaderBoard();
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void OnPayBillsButtonClicked()
    {
        GameManager.Instance.paidBillsToday = true;
        //GameManager.Instance.objectiveVisible = false;
        ObjectiveDisplay.Instance.SetNeutral();
        Player.Instance.SubtractMoney(GameManager.Instance.targetMoney);
        GameManager.Instance.targetMoney += 25;
        Player.Instance.Day += 1;
        Player.Instance.ResetDailyStats();
        DayDisplay.Instance.animateFade();
        AudioManager.Instance.PlayDayTransition();
        DesktopManager.Instance.DesktopIcons[2].GetComponent<Button>().interactable = true;
        gameObject.GetComponent<Button>().interactable = false;
        DesktopManager.Instance.AddReminderMail();
    }
}
