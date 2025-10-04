using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int Money;
    public int Turn;
    public int Day;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubtractTurn()
    {
        Turn -= 1;
        if (Turn <= 0)
        {
            // End the game if turns run out
            Debug.Log("Game Over! You have run out of turns.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
    }
}
