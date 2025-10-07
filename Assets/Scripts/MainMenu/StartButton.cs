using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene("PrologueScene");
    }
}
