using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static Map Instance;
    public MapNode playerStartNode;
    public MapNode[] eventNodes;
    private int NumberOfEasyDilemmas;
    private int NumberOfHardDilemmas;
    private int NumberOfEasyGigs;
    private int NumberOfHardGigs;

    private List<MapNode> MapNodes = new List<MapNode>();
    private MapNode PlayerNode;

    private void Start()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        MapNodes.Add(playerStartNode);
        MapNodes.AddRange(eventNodes);

        InitializeNewMap();

        AudioManager.Instance.PlayMapMusic();
    }


    private void InitializeNewMap()
    {
        PlayerNode = MapNodes[0];
        AssignEvents();
    }

    private void AssignEvents()
    {
        
        MapNode.NodeType[] events = new MapNode.NodeType[eventNodes.Length];

        if (Player.Instance.Day < 2)
        {
            NumberOfEasyDilemmas = 4;
            NumberOfHardDilemmas = 0;
            NumberOfEasyGigs = 3;
            NumberOfHardGigs = 0;
        } 
        else if (Player.Instance.Day < 4)
        {
            NumberOfEasyDilemmas = 3;
            NumberOfHardDilemmas = 1;
            NumberOfEasyGigs = 2;
            NumberOfHardGigs = 1;
        }
        else
        {
            NumberOfEasyDilemmas = 1;
            NumberOfHardDilemmas = 3;
            NumberOfEasyGigs = 1;
            NumberOfHardGigs = 2;
        }

        // Create event array
        int index = 0;
        for (int i = 0; i < NumberOfEasyDilemmas; i++)
        {
            events[index++] = MapNode.NodeType.EasyDilemma;
        }
        for (int i = 0; i < NumberOfHardDilemmas; i++)
        {
            events[index++] = MapNode.NodeType.HardDilemma;
        }
        for (int i = 0; i < NumberOfEasyGigs; i++)
        {
            events[index++] = MapNode.NodeType.EasyGig;
        }
        for (int i = 0; i < NumberOfHardGigs; i++)
        {
            events[index++] = MapNode.NodeType.HardGig;
        }

        // Shuffle events using Fisher-Yates algorithm
        for (int i = 0; i < events.Length; i++)
        {
            MapNode.NodeType temp = events[i];
            int randomIndex = Random.Range(i, events.Length);
            events[i] = events[randomIndex];
            events[randomIndex] = temp;
        }

        // Assign events to nodes
        int eventIndex = 0;
        for (int i = 0; i < MapNodes.Count; i++)
        {
            if (MapNodes[i] != PlayerNode)
            {
                MapNodes[i].AssignEvent(events[eventIndex]);
                eventIndex++;
            }
            else
            {
                MapNodes[i].AssignEvent(MapNode.NodeType.Player);
            }
        }

        UpdateAccessibleNodes();

        ObjectiveDisplay.Instance.SetObjective();
    }

    private void UpdateAccessibleNodes()
    {
        // Disable all nodes
        foreach (MapNode node in MapNodes)
        {
            node.DisableNode();
        }

        // Enable accessible nodes
        foreach (MapNode node in PlayerNode.accessibleNodes)
        {
            node.EnableNode();
        }

        // Enable player node
        PlayerNode.EnableNode();
    }

    public void MoveToNode(MapNode newNode)
    {
        // Mark old player node as completed
        if (PlayerNode != null)
        {
            PlayerNode.nodeType = MapNode.NodeType.Completed;
            PlayerNode.UpdateVisual();
        }

        // Update new node to player
        newNode.nodeType = MapNode.NodeType.Player;
        newNode.UpdateVisual();

        // Update player position
        PlayerNode = newNode;

        UpdateAccessibleNodes();
    }
}
