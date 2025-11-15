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
    public int NumberOfEasyDilemmas = 2;
    public int NumberOfHardDilemmas = 2;
    public int NumberOfEasyGigs = 1;
    public int NumberOfHardGigs = 2;

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
    }


    private void InitializeNewMap()
    {
        PlayerNode = MapNodes[0];
        AssignEvents();
    }

    private void AssignEvents()
    {
        
        MapNode.NodeType[] events = new MapNode.NodeType[eventNodes.Length];

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
