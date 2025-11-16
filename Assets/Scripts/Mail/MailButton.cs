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
        Player.Instance.SubtractMoney(GameManager.Instance.targetMoney);
        GameManager.Instance.targetMoney += 25;
        DesktopManager.Instance.DesktopIcons[2].GetComponent<Button>().interactable = true;
        gameObject.GetComponent<Button>().interactable = false;
    }
}
