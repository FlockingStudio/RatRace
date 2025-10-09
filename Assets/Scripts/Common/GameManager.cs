using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject MenuPrefab;

    public enum Stage
    {
        map = 0,
        gig = 1,
        dilemma = 2,
        prologue= 3
    }

    public Dictionary<string, MapNode.NodeType> NodeStates { get; set; }
    public bool IsGameOver { get; set; } = false;
    private GameObject menuInstance;

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

        // Initialize game state
        NodeStates = new Dictionary<string, MapNode.NodeType>();
    }

    public void OpenGig() => OpenScene(Stage.gig);

    public void OpenDilemma() => OpenScene(Stage.dilemma);

    public void OpenMap() => OpenScene(Stage.map);
    public void OpenPrologue() => OpenScene(Stage.prologue);

    private void OpenScene(Stage stage)
    {
        string sceneName = GetSceneName(stage);
        StartCoroutine(CloseAllWindowsAndLoadScene(sceneName));
    }

    private string GetSceneName(Stage stage)
    {
        switch (stage)
        {
            case Stage.map:
                return "MapScene";
            case Stage.gig:
                return "GigScene";
            case Stage.dilemma:
                return "DilemmaScene";
            case Stage.prologue:
                return "PrologueScene";
            default:
                return "MapScene";
        }
    }

    private IEnumerator CloseAllWindowsAndLoadScene(string sceneName)
    {
        Window[] allWindows = FindObjectsOfType<Window>();

        foreach (Window window in allWindows)
        {
            window.CloseWindow();
        }

        // Wait for window close animation to complete (0.8s standard duration)
        if (allWindows.Length > 0)
        {
            yield return new WaitForSeconds(0.8f);
        }

        SceneManager.LoadScene(sceneName);
    }

    public void OpenMenu()
    {
        // instantiate menu prefab in the Screen canvas
        if (menuInstance != null) return;
        menuInstance = Instantiate(MenuPrefab, GameObject.Find("Screen").transform);
        SoundManager.Instance.PauseBackgroundMusic();
    }

    public void RestartGame()
    {
        Destroy(Player.Instance.gameObject);
        Destroy(SoundManager.Instance.gameObject);
        Destroy(Instance.gameObject);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ResumeGame()
    {
        menuInstance.GetComponent<Window>().CloseWindow();
        SoundManager.Instance.PlayBackgroundMusic();
    }

    public void MoveToNextDay()
    {
        NodeStates.Clear();
        Player.Instance.Day += 1;
        Player.Instance.Turn = 3;
        IsGameOver = true;
        OpenMap();
    }
}
