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

    public void AddTurn(int amount)
    {
        Turn += amount;
        NumberFlash TurnFlash = GameObject.Find("TurnFlash").GetComponent<NumberFlash>();
        TurnFlash.Activate(amount);
    }

    public void SubtractTurn(int amount)
    {
        Turn -= amount;
        if (Turn < 0)
        {
            Turn = 0;
        }
        NumberFlash TurnFlash = GameObject.Find("TurnFlash").GetComponent<NumberFlash>();
        TurnFlash.Activate(-amount);
    }

    public int GetMoney()
    {
        return Money;
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        SoundManager.Instance.PlayCoinSoundGain();
        NumberFlash MoneyFlash = GameObject.Find("MoneyFlash").GetComponent<NumberFlash>();
        MoneyFlash.Activate(amount);
    }

    public void SubtractMoney(int amount)
    {
        NumberFlash MoneyFlash = GameObject.Find("MoneyFlash").GetComponent<NumberFlash>();
        MoneyFlash.Activate(-amount);
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
