using System.Collections;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private bool playOnStart = false;

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Vector2 originalPosition;
    private Coroutine currentAnimation;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.anchoredPosition;
    }

    void Start()
    {
        if (playOnStart)
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        StopAnimation();

        // Capture current position as the base position for animations
        originalPosition = rectTransform.anchoredPosition;
        originalScale = rectTransform.localScale;
        currentAnimation = StartCoroutine(BounceAnimation());
    }

    public void StopAnimation()
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }

        // Reset to original state
        rectTransform.localScale = originalScale;
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localRotation = Quaternion.identity;
    }

    private IEnumerator BounceAnimation()
    {
        float bounceHeight = 20f;

        while (true)
        {
            float time = Time.time * animationSpeed * 3f;
            float bounce = Mathf.Abs(Mathf.Sin(time)) * bounceHeight;
            rectTransform.anchoredPosition = originalPosition + new Vector2(0, bounce);
            yield return null;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation()
    {
        // Start from small scale
        rectTransform.localScale = Vector3.zero;

        float elapsed = 0f;
        float duration = 0.3f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Ease out back for a bouncy effect
            float overshoot = 1.70158f;
            float scale = 1f + overshoot * Mathf.Pow(t - 1f, 3f) + overshoot * Mathf.Pow(t - 1f, 2f);
            rectTransform.localScale = originalScale * scale;
            yield return null;
        }

        rectTransform.localScale = originalScale;

        if (playOnStart)
        {
            StartAnimation();
        }
    }
}
