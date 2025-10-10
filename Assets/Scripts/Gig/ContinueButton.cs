using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public void SwitchToMap()
    {
        GameManager.Instance.OpenMap();
    }
}
