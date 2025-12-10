using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaGameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DesktopManager.Instance.Busy = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        //GameManager.Instance.busy = true;
    }

    public void OnClick()
    {
        if (Player.Instance.GetMoney() >= GameManager.Instance.targetMoney)
        {
            DesktopManager.Instance.NextDaySequence();
        }
        else
        {
            DesktopManager.Instance.EndSequence();
        }
    }
}
