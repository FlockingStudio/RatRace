using System.Collections;
using UnityEngine;

public class ChildToggle : MonoBehaviour
{
    [Header("Child References")]
    [SerializeField] private GameObject childOne;
    [SerializeField] private GameObject childTwo;

    [Header("Initial State")]
    [SerializeField] private bool childOneActiveOnStart = true;

    void Start()
    {
        // Auto-assign children if not set in inspector
        if (childOne == null && transform.childCount > 0)
        {
            childOne = transform.GetChild(0).gameObject;
        }

        if (childTwo == null && transform.childCount > 1)
        {
            childTwo = transform.GetChild(1).gameObject;
        }

        // Validate
        if (childOne == null || childTwo == null)
        {
            enabled = false;
            return;
        }

        // Set initial state
        if (childOneActiveOnStart)
        {
            EnableChildOne();
        }
        else
        {
            EnableChildTwo();
        }

        // Pause for 3 seconds and toggle
        StartCoroutine(PauseAndToggle(3f));
    }

    /// <summary>
    /// Waits for specified seconds then toggles to the other child
    /// </summary>
    public IEnumerator PauseAndToggle(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Toggle();
    }

    public IEnumerator ToggleAndPause(float seconds)
    {
        Toggle();
        yield return new WaitForSeconds(seconds);
    }

    /// <summary>
    /// Coroutine that toggles, waits, then loads the Home scene
    /// </summary>
    private IEnumerator ToggleWaitAndLoadHome(float seconds)
    {
        Toggle();
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.OpenHome();
    }

    /// <summary>
    /// Wrapper for OnEndEdit - toggles, shows loading for 3 seconds, then loads Home scene
    /// Call this from InputField's OnEndEdit event
    /// </summary>
    public void OnEndEditToggleWithDelay()
    {
        StartCoroutine(ToggleWaitAndLoadHome(3f));
    }

    /// <summary>
    /// Enables first child, disables second child
    /// </summary>
    public void EnableChildOne()
    {
        childOne.SetActive(true);
        childTwo.SetActive(false);
    }

    /// <summary>
    /// Enables second child, disables first child
    /// </summary>
    public void EnableChildTwo()
    {
        childOne.SetActive(false);
        childTwo.SetActive(true);
    }

    /// <summary>
    /// Toggles between the two children
    /// </summary>
    public void Toggle()
    {
        if (childOne.activeSelf)
        {
            SoundManager.Instance.PlayMailSound();
            EnableChildTwo();
        }
        else
        {
            SoundManager.Instance.PlayTrophySound();
            EnableChildOne();
        }
    }
}
