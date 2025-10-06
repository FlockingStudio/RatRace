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
        dilemma = 2
    }

    public Stage CurrentStage { get; set; }

    public Dictionary<string, MapNode.NodeType> NodeStates { get; set; }
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
        CurrentStage = Stage.map;
        NodeStates = new Dictionary<string, MapNode.NodeType>();
    }

    public void OpenGig() => OpenScene(Stage.gig);

    public void OpenDilemma() => OpenScene(Stage.dilemma);

    public void OpenMap() => OpenScene(Stage.map);

    private void OpenScene(Stage stage)
    {
        CurrentStage = stage;
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
        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene(sceneName);
    }

    public void OpenMenu()
    {
        // instantiate menu prefab in the Screen canvas
        if (menuInstance != null) return;
        menuInstance = Instantiate(MenuPrefab, GameObject.Find("Screen").transform);
    }

    public void RestartGame()
    {
        Destroy(Player.Instance.gameObject);
        Destroy(Instance.gameObject);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ResumeGame()
    {
        menuInstance.GetComponent<Window>().CloseWindow();
    }
}
