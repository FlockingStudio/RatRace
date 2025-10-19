using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button rollButton;
    public void SwitchToMap()
    {
        GameManager.Instance.OpenMap();
        rollButton.interactable = false;
        GetComponent<Button>().interactable = false;
        
    }
}
