using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int Money { get; set; }
    public int Turn { get; set; }

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

        Money = 100;
        Turn = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
