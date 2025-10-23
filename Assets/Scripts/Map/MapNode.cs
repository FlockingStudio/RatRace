using System;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public enum NodeType { None, Gig, Dilemma, Player, Completed }

    public MapNode[] accessibleNodes;

    public NodeType nodeType = NodeType.None;

    [Header("Float Animation")]
    public float floatAmplitude = 10f;

    public float floatSpeed = 2f;

    private RectTransform rectTransform;
    private Vector2 startPosition;
    private bool isFloating = false;
    private bool isInitialized = false;

    private void Awake()
    {
        InitializeRectTransform();
    }

    private void Update()
    {
        if (isFloating && nodeType != NodeType.Player && nodeType != NodeType.Completed)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            rectTransform.anchoredPosition = new Vector2(startPosition.x, newY);
        }
    }

    private void InitializeRectTransform()
    {
        if (!isInitialized)
        {
            rectTransform = GetComponent<RectTransform>();
            startPosition = rectTransform.anchoredPosition;
            isInitialized = true;
        }
    }

    public void AssignEvent(NodeType eventType)
    {
        nodeType = eventType;
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        Image img = GetComponent<Image>();
        switch (nodeType)
        {
            case NodeType.Gig:
                img.sprite = Resources.Load<Sprite>("Map_Icon_Gig");
                break;
            case NodeType.Dilemma:
                img.sprite = Resources.Load<Sprite>("Map_Icon_Dilemma");
                break;
            case NodeType.Player:
                img.sprite = Resources.Load<Sprite>("Map_Icon_Player");
                break;
            case NodeType.Completed:
                img.sprite = Resources.Load<Sprite>("Map_Icon_Complete");
                break;
        }
    }

    public void DisableNode()
    {
        InitializeRectTransform();

        Image img = GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.25f);

        Button btn = GetComponent<Button>();
        btn.interactable = false;

        isFloating = false;
        rectTransform.anchoredPosition = startPosition;
    }

    public void EnableNode()
    {
        Image img = GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);

        Button btn = GetComponent<Button>();
        btn.interactable = true;

        isFloating = nodeType != NodeType.Player && nodeType != NodeType.Completed;
    }

    public bool IsAccessible()
    {
        return GetComponent<Button>().interactable;
    }

    public void OnNodeClicked()
    {
        if (IsAccessible() && nodeType != NodeType.Player)
        {
            // Remember the original node type before moving
            NodeType originalType = nodeType;

            // Move to the node first
            Map.Instance.MoveToNode(this);

            // Subtract turn
            Player.Instance.SubtractTurn(1);

            if (Player.Instance.Turn < 1 && originalType == NodeType.Completed)
            {
                GameManager.Instance.IsDayOver = true;
                GameManager.Instance.OpenGameOver();
                return;
            }

            // Open appropriate scene based on original node type
            switch (originalType)
            {
                case NodeType.Gig:
                    GameManager.Instance.OpenGig();
                    break;

                case NodeType.Dilemma:
                    GameManager.Instance.OpenDilemma();
                    break;

                case NodeType.Completed:
                    // Just stay on map, already moved
                    break;

                default:
                    throw new Exception("Node type not recognized");
            }
        }
    }
}
