using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardRetry : MonoBehaviour
{
    // Start is called before the first frame update
    public void retry()
    {
        GameManager.Instance.RestartGame();
    }
}
