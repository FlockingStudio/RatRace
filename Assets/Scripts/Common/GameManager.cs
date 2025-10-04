using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Stage CurrentStage { get; set; }

    // Store the state of each node by its name
    public Dictionary<string, MapNode.NodeType> NodeStates { get; set; }
    public enum Stage
    {
        map = 0,
        gig = 1,
        dilemma = 2
    }

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
        CurrentStage = Stage.map;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGig() => OpenScene(Stage.gig);
    public void OpenDilemma() => OpenScene(Stage.dilemma);
    public void OpenMap() => OpenScene(Stage.map);
    private void OpenScene(Stage stage)
    {
        CurrentStage = stage;
        string sceneName = "";
        switch (stage)
        {
            case Stage.map:
                sceneName = "MapScene";
                break;
            case Stage.gig:
                sceneName = "GigScene";
                break;
            case Stage.dilemma:
                sceneName = "DilemmaScene";
                break;
        }

        StartCoroutine(CloseAllWindowsAndLoadScene(sceneName));
    }

    private IEnumerator CloseAllWindowsAndLoadScene(string sceneName)
    {
        Window[] allWindows = FindObjectsOfType<Window>();

        foreach (Window window in allWindows)
        {
            window.CloseWindow();
        }

        // Wait for animation to complete (0.4f is the duration from MinimizeAnimation)
        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene(sceneName);
    }
}
