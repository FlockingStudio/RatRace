using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public static int money;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        money = 200;
        // if not in To scene load scene
        if (SceneManager.GetActiveScene().name != "GigScene")
        {
            SceneManager.LoadScene("GigScene");
        }
    }
}
