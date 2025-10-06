using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DilemmaButton : MonoBehaviour
{
    public AudioSource clickSound;
    int cost;
    public Button self;
    public Button other;

    public void SetCost(int num)
    {
        cost = num;
    }
    
    public int GetCost() 
    {
        return cost; 
    }

    void SwitchToMap()
    {
        GameManager.Instance.OpenMap();
    }
    //main fuctionality. changes money, plays audio, switches to map, and disables the button (prevent spamming to get a lot of money)
    public void ModMoney()
    {
        Player.Instance.Money += cost;
        Invoke("SwitchToMap", 1.0f);
        clickSound.Play();
        self.enabled = false;
        other.enabled = false;
    }
}
