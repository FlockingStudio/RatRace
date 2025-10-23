using UnityEngine;
using UnityEngine.UI;

public class PrologueButton : MonoBehaviour
{
    public void PressButton()
    {
            GameManager.Instance.OpenMap();
            // disable button to prevent spamming
            GetComponent<Button>().enabled = false;
    }

    void Start()
    {
    }
}
