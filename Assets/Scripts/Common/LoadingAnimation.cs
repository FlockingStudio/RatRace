using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Sprite[] frames; // Assign your loading sprites here
    [SerializeField] private float frameRate = 0.1f; // Time between frames (seconds)

    private Image imageComponent;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        imageComponent = GetComponent<Image>();

        if (frames.Length == 0)
        {
            Debug.LogError("No frames assigned to LoadingAnimation!");
            enabled = false;
            return;
        }

        // Set initial frame
        if (imageComponent != null)
        {
            imageComponent.sprite = frames[0];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if it's time to change frame
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length; // Loop back to 0 after last frame
            imageComponent.sprite = frames[currentFrame];
        }
    }

    // Optional: Method to change animation speed at runtime
    public void SetFrameRate(float newFrameRate)
    {
        frameRate = newFrameRate;
    }
}
