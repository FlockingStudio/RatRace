using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DilemmaButton : MonoBehaviour
{
    int moneyCost;
    int turnCost;
    public Button self;
    public Button other;

    public void SetMoneyCost(int num)
    {
        moneyCost = num;
    }

    public int GetMoneyCost()
    {
        return moneyCost;
    }

    public void SetTurnCost(int num)
    {
        turnCost = num;
    }

    void SwitchToMap()
    {
        GameManager.Instance.OpenMap();
    }
    //main fuctionality. changes money, plays audio, switches to map, and disables the button (prevent spamming to get a lot of money)
    public void ModMoney()
    {
        if (moneyCost < 0)
        {
            Player.Instance.SubtractMoney(-moneyCost);
        }
        else if (moneyCost > 0)
        {
            Player.Instance.AddMoney(moneyCost);
        }
        if (turnCost < 0)
        {
            Player.Instance.SubtractTurn(turnCost);

        }
        else if (turnCost > 0)
        {
            Player.Instance.AddTurn(turnCost);
        }

        Invoke("SwitchToMap", 1.0f);
        self.enabled = false;
        other.enabled = false;
    }
}
