using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject boundaryObject;
    private RectTransform rectTransform;
    private RectTransform boundaryRectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (boundaryObject != null)
        {
            boundaryRectTransform = boundaryObject.GetComponent<RectTransform>();
        }
        else
        {
            // grab the componnet using the tag "boundary"
            GameObject boundary = GameObject.FindGameObjectWithTag("Boundary");
            if (boundary != null)
            {
                boundaryRectTransform = boundary.GetComponent<RectTransform>();
            }
        }
    }

    private void Start()
    {
        MaximizeWindow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition + (eventData.delta / canvas.scaleFactor);

        if (boundaryRectTransform != null)
        {
            newPosition = ClampToBoundary(newPosition);
        }

        rectTransform.anchoredPosition = newPosition;
    }

    private Vector2 ClampToBoundary(Vector2 position)
    {
        Vector2 boundarySize = boundaryRectTransform.sizeDelta;
        Vector2 objectSize = rectTransform.sizeDelta;
        Vector2 boundaryPos = boundaryRectTransform.anchoredPosition;

        float minX = boundaryPos.x - (boundarySize.x / 2) + (objectSize.x / 2);
        float maxX = boundaryPos.x + (boundarySize.x / 2) - (objectSize.x / 2);
        float minY = boundaryPos.y - (boundarySize.y / 2) + (objectSize.y / 2);
        float maxY = boundaryPos.y + (boundarySize.y / 2) - (objectSize.y / 2);

        return new Vector2(Mathf.Clamp(position.x, minX, maxX), Mathf.Clamp(position.y, minY, maxY));
    }

    public void CloseWindow() => StartCoroutine(AnimateWindow(true));

    public void MaximizeWindow() => StartCoroutine(AnimateWindow(false));

    private IEnumerator AnimateWindow(bool minimize)
    {
        Vector2 startPos = minimize ? rectTransform.anchoredPosition : new Vector2(0, -Screen.height * 0.4f);
        Vector2 targetPos = minimize ? new Vector2(0, -Screen.height * 0.4f) : rectTransform.anchoredPosition;
        Vector3 startScale = minimize ? rectTransform.localScale : Vector3.zero;
        Vector3 targetScale = minimize ? Vector3.zero : Vector3.one;

        if (!minimize)
        {
            rectTransform.anchoredPosition = startPos;
            rectTransform.localScale = startScale;
        }

        float duration = 0.4f;
        for (float elapsed = 0; elapsed < duration; elapsed += Time.deltaTime)
        {
            float t = elapsed / duration;
            float curve = minimize ? t * t : 1f - (1f - t) * (1f - t);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, curve);
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, curve);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        rectTransform.localScale = targetScale;

        if (minimize)
        {
            Destroy(gameObject);
        }
    }
}
