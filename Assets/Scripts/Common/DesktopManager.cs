using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WindowType
{
    Gig,
    Mail,
    Map,
    Dilemma
}

public class DesktopManager : MonoBehaviour
{
    public static DesktopManager Instance { get; private set; }
    public GameObject GigWindowPrefab;
    public GameObject MailWindowPrefab;
    public GameObject MapWindowPrefab;
    public Icon[] DesktopIcons;
    private Canvas canvas;
    private Dictionary<WindowType, GameObject> windows = new Dictionary<WindowType, GameObject>();
    private Dictionary<WindowType, GameObject> prefabs = new Dictionary<WindowType, GameObject>();
    


    // Start is called before the first frame update
    void Start()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        canvas = GetComponentInParent<Canvas>();

        // Initialize prefabs dictionary
        prefabs[WindowType.Gig] = GigWindowPrefab;
        prefabs[WindowType.Mail] = MailWindowPrefab;
        prefabs[WindowType.Map] = MapWindowPrefab;
        prefabs[WindowType.Dilemma] = GigWindowPrefab; // Note: uses GigWindowPrefab
    }

    public void OpenWindow(WindowType type)
    {
        if (!windows.ContainsKey(type) || windows[type] == null)
        {
            windows[type] = Instantiate(prefabs[type], canvas.transform);
        }
        else
        {
            if (!windows[type].activeSelf)
            {

                windows[type].SetActive(true);
                windows[type].GetComponent<Window>().MaximizeWindow();
            }
        }
    }

    public void CloseWindow(WindowType type)
    {
        if (windows.ContainsKey(type) && windows[type] != null)
        {
            Destroy(windows[type]);
            windows[type] = null;
        }
    }

    public void ShowIcons()
    {
        foreach (Icon icon in DesktopIcons)
        {
            if (icon.gameObject.activeSelf == false)
            {
                icon.Show();
            }
        }
    }

    // Legacy methods for backward compatibility
    public void OpenMail() => OpenWindow(WindowType.Mail);
    public void OpenGig() => OpenWindow(WindowType.Gig);
    public void OpenDilemma() => OpenWindow(WindowType.Dilemma);
    public void OpenMap() => OpenWindow(WindowType.Map);

    public void CloseMail() => CloseWindow(WindowType.Mail);
    public void CloseGig() => CloseWindow(WindowType.Gig);
    public void CloseDilemma() => CloseWindow(WindowType.Dilemma);
    public void CloseMap() => CloseWindow(WindowType.Map);

}
