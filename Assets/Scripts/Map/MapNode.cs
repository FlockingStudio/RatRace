using System;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public enum NodeType { None, EasyGig, HardGig, EasyDilemma, HardDilemma, Player, Completed }

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
            case NodeType.EasyDilemma:
                img.sprite = Resources.Load<Sprite>("Sprites/Map/Green_QuestionMark");
                break;
            case NodeType.HardDilemma:
                img.sprite = Resources.Load<Sprite>("Sprites/Map/Red_QuestionMark");
                break;
            case NodeType.EasyGig:
                img.sprite = Resources.Load<Sprite>("Sprites/Map/Green_Dice");
                break;
            case NodeType.HardGig:
                img.sprite = Resources.Load<Sprite>("Sprites/Map/Red_Dice");
                break;
            case NodeType.Player:
                img.sprite = Resources.Load<Sprite>("Sprites/Map/PlayerIcon");
                break;
            case NodeType.Completed:
                img.sprite = Resources.Load<Sprite>("Sprites/Map/Map_Icon_Complete");
                break;
        }
    }

    public void DisableNode()
    {
        InitializeRectTransform();

        Button btn = GetComponent<Button>();
        btn.interactable = false;

        isFloating = false;
        rectTransform.anchoredPosition = startPosition;
    }

    public void EnableNode()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = true;

        isFloating = nodeType != NodeType.Player && nodeType != NodeType.Completed;
    }

    public void OnNodeClicked()
    {
        if (DesktopManager.Instance.Busy)
        {
            return;
        }

        if (nodeType != NodeType.Player)
        {
            // Remember the original node type before moving
            NodeType originalType = nodeType;

            // Move to the node first
            Map.Instance.MoveToNode(this);

            // Subtract turn
            Player.Instance.SubtractTurn(1);

            // out of turn + clicking visited node
            if (Player.Instance.Turn < 1 && originalType == NodeType.Completed)
            {
                DesktopManager.Instance.OpenMetagameEnd();
                return;
            }

            // Open appropriate scene based on original node type
            switch (originalType)
            {
                case NodeType.EasyGig:
                    Player.Instance.EventDifficulty = Difficulty.EASY;
                    DesktopManager.Instance.OpenGig();
                    break;
                case NodeType.HardGig:
                    Player.Instance.EventDifficulty = Difficulty.HARD;
                    DesktopManager.Instance.OpenGig();
                    break;

                case NodeType.EasyDilemma:
                    Player.Instance.EventDifficulty = Difficulty.EASY;
                    DesktopManager.Instance.OpenDilemma();
                    break;
                case NodeType.HardDilemma:
                    Player.Instance.EventDifficulty = Difficulty.HARD;
                    DesktopManager.Instance.OpenDilemma();
                    break;

                case NodeType.Completed:
                    break;

                default:
                    throw new Exception("Node type not recognized: " + originalType);
            }
        }
    }
}
