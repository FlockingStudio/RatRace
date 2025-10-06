using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public int Money;

    public int Turn;

    public int Day;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubtractTurn()
    {
        Turn -= 1;

        if (Turn <= 0)
        {
            Debug.Log("Game Over! You have run out of turns.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }
}
