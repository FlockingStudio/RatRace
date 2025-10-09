using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DilemmaButton : MonoBehaviour
{
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
        if (cost < 0)
        {
            Player.Instance.SubtractMoney(-cost);

        }
        else if (cost > 0)
        {
            Player.Instance.AddMoney(cost);
        }
        
        Invoke("SwitchToMap", 1.0f);
        self.enabled = false;
        other.enabled = false;
    }
}
