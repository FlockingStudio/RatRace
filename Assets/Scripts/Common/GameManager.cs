using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject MenuPrefab;

    public enum Stage
    {
        map = 0,
        gig = 1,
        dilemma = 2,
        prologue = 3,
        login = 4,
        gameOver = 5,
        home = 6
    }

    private GameObject menuInstance;

    // minimum money to win the game
    public int targetMoney = 50;
    public bool paidBillsToday = false;

    // csv pooling

    // dilemma
    public TextAsset easyDilemmaCSV;
    public TextAsset hardDilemmaCSV;
    public CSVPool easyDilemmaPool;
    public CSVPool hardDilemmaPool;

    // gig
    public TextAsset easyGigCSV;
    public TextAsset hardGigCSV;
    public CSVPool easyGigPool;
    public CSVPool hardGigPool;

    // mail
    public TextAsset subscriptionMailCSV;
    public TextAsset specialMailCSV;
    public CSVPool subscriptionMailPool;
    public CSVPool specialMailPool;

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

    void Start()
    {
        LoadCsvPools();
    }

    public void OpenHome() => OpenScene(Stage.home);
    public void OpenLeaderBoard() => OpenScene(Stage.gameOver);

    private void OpenScene(Stage stage)
    {
        string sceneName = GetSceneName(stage);
        StartCoroutine(CloseAllWindowsAndLoadScene(sceneName));
    }

    private string GetSceneName(Stage stage)
    {
        switch (stage)
        {
            case Stage.login:
                return "Login";
            case Stage.gameOver:
                return "LeaderboardScene";
            case Stage.home:
                return "Desktop";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator CloseAllWindowsAndLoadScene(string sceneName)
    {
        Window[] allWindows = FindObjectsOfType<Window>();

        if (allWindows.Length > 0)
        {
            float delayBetweenWindows = 1.5f / allWindows.Length;

            foreach (Window window in allWindows)
            {
                window.CloseWindow();
                yield return new WaitForSeconds(delayBetweenWindows);
            }
        }

        SceneManager.LoadScene(sceneName);
    }

    public void OpenMenu()
    {
        // instantiate menu prefab in the Screen canvas
        if (menuInstance != null) return;
        Button[] allButtons = UnityEngine.Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in allButtons)
        {
            button.enabled = false;
        }
        //GameManager.Instance.busy = true;
        menuInstance = Instantiate(MenuPrefab, GameObject.Find("Screen").transform);
    }

    public void RestartGame()
    {
        Destroy(Player.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(Instance.gameObject);
        SceneManager.LoadScene("Login");
    }

    public void ResumeGame()
    {
        menuInstance.GetComponent<Window>().CloseWindow();
        Button[] allButtons = UnityEngine.Object.FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in allButtons)
        {
            button.enabled = true;
        }
        //GameManager.Instance.busy = false;
    }

    private void LoadCsvPools()
    {
        easyDilemmaPool = new CSVPool(easyDilemmaCSV);
        hardDilemmaPool = new CSVPool(hardDilemmaCSV);
        easyGigPool = new CSVPool(easyGigCSV);
        hardGigPool = new CSVPool(hardGigCSV);
        subscriptionMailPool = new CSVPool(subscriptionMailCSV);
        specialMailPool = new CSVPool(specialMailCSV);
    }
}
