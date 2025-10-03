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
}
