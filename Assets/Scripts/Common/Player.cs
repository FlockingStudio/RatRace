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

    public void SubtractTurns(int change)
    {
        Turn -= change;

        if (Turn <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }

    public void AddTurns(int change)
    {
        Turn += change;
    }

    public int GetMoney()
    {
        return Money;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        SoundManager.Instance.PlayCoinSoundGain();
        MoneyFlash.Instance.Activate(amount);
    }

    public void SubtractMoney(int amount)
    {
        MoneyFlash.Instance.Activate(-amount);
        //i'm putting it like this because i don't think we should play the sound if you don't actually lose money -craig
        if (Money != 0)
        {
            SoundManager.Instance.PlayCoinSoundLose();
        }
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
