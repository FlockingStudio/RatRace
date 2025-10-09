using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int Money;

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
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }

    public int GetMoney()
    {
        return Money;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        SoundManager.Instance.PlayCoinSound();
    }

    public void SubtractMoney(int amount)
    {
        if (Money <= amount)
        {
            Money = 0;
        }
        else
        {
            Money -= amount;
        }
    }
}
