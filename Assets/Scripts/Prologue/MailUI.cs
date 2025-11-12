using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//new Vector3(GameObject.Find("Screen").transform.position.x + 10, GameObject.Find("Screen").transform.position.y + 10, GameObject.Find("Screen").transform.position.z)
public class MailUI : MonoBehaviour
{
    public GameObject mailWindowPrefab;
    public bool mailOpen = false;

    /*void LandlordEmail()
    {
        GameObject landlordWindow;
        if (!landlordOpen)
        {
            landlordWindow = Instantiate(landlordWindowPrefab, GameObject.Find("Screen").transform);
            landlordOpen = true;
        }
    }*/

    public void OpenEmail()
    {
        GameObject mailWindow;
        if (!mailOpen)
        {
            mailWindow = Instantiate(mailWindowPrefab, GameObject.Find("Screen").transform);
            mailOpen = true;
        }
    }
}
