using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static Map Instance;
    public GameObject TechDemoEndPrefab;

    public int NumberOfDilemmas;

    public int NumberOfGigs;

    private MapNode[] MapNodes;
    private MapNode PlayerNode;

    private void Start()
    {
        MapNodes = FindObjectsOfType<MapNode>();
        Instance = this;

        // Check if map state was previously saved
        if (GameManager.Instance.NodeStates.Count == MapNodes.Length)
        {
            RestoreMapState();
        }
        else
        {
            ValidateEventConfiguration();
            InitializeNewMap();
        }

        if (PlayerNode != null)
        {
            UpdateAccessibleNodes();
        }
        // Changes the sound track playing
        SoundManager.Instance.SwitchBackgroundMusic(GameManager.Stage.map);
    }

    private void OnDestroy()
    {
        if (!GameManager.Instance.IsDayOver)
            SaveMapState();
    }

    private void RestoreMapState()
    {
        foreach (MapNode node in MapNodes)
        {
            MapNode.NodeType state = GameManager.Instance.NodeStates[node.name];
            node.AssignEvent(state);

            if (state == MapNode.NodeType.Player)
            {
                PlayerNode = node;
            }
        }
    }

    private void ValidateEventConfiguration()
    {
        if (NumberOfDilemmas + NumberOfGigs != MapNodes.Length - 1)
        {
            Debug.LogError("Number of events does not match number of nodes minus one for player start node.");
        }
    }

    private void InitializeNewMap()
    {
        RandomlyPlacePlayer();
        AssignEvents();
    }

    private void RandomlyPlacePlayer()
    {
        int randomIndex = Random.Range(0, MapNodes.Length);
        PlayerNode = MapNodes[randomIndex];
        PlayerNode.nodeType = MapNode.NodeType.Player;
    }

    private void AssignEvents()
    {
        MapNode.NodeType[] events = new MapNode.NodeType[NumberOfDilemmas + NumberOfGigs];

        // Create event array
        for (int i = 0; i < NumberOfDilemmas; i++)
        {
            events[i] = MapNode.NodeType.Dilemma;
        }
        for (int i = NumberOfDilemmas; i < NumberOfDilemmas + NumberOfGigs; i++)
        {
            events[i] = MapNode.NodeType.Gig;
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
        for (int i = 0; i < MapNodes.Length; i++)
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
            GameManager.Instance.NodeStates[PlayerNode.name] = MapNode.NodeType.Completed;
        }

        // Update new node to player
        newNode.nodeType = MapNode.NodeType.Player;
        newNode.UpdateVisual();
        GameManager.Instance.NodeStates[newNode.name] = MapNode.NodeType.Player;

        // Update player position
        PlayerNode = newNode;

        UpdateAccessibleNodes();
    }

    private void SaveMapState()
    {
        foreach (MapNode node in MapNodes)
        {
            GameManager.Instance.NodeStates[node.name] = node.nodeType;
        }
    }
}
