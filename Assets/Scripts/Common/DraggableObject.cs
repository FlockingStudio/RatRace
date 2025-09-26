using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject boundaryObject;
    private RectTransform rectTransform;
    private RectTransform boundaryRectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        if (boundaryObject != null)
        {
            boundaryRectTransform = boundaryObject.GetComponent<RectTransform>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
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

        float minX = boundaryRectTransform.anchoredPosition.x - (boundarySize.x / 2) + (objectSize.x / 2);
        float maxX = boundaryRectTransform.anchoredPosition.x + (boundarySize.x / 2) - (objectSize.x / 2);
        float minY = boundaryRectTransform.anchoredPosition.y - (boundarySize.y / 2) + (objectSize.y / 2);
        float maxY = boundaryRectTransform.anchoredPosition.y + (boundarySize.y / 2) - (objectSize.y / 2);

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        return position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}