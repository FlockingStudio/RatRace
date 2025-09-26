using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int Money { get; set; }
    public int Day { get; set; }
    public Stage CurrentStage { get; set; }
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

        // initialize values
        CurrentStage = Stage.map;
        Money = 100;
        Day = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGig()
    {
        CurrentStage = Stage.gig;
        StartCoroutine(CloseAllWindowsAndLoadScene("GigScene"));
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
