using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public int NumberOfDilemmas;
    public int NumberOfGigs;
    private MapNode[] MapNodes;
    private MapNode PlayerNode;
    // Start is called before the first frame update
    void Start()
    {
        MapNodes = FindObjectsOfType<MapNode>();

        if (GameManager.Instance.NodeStates.Count == MapNodes.Length)
        {
            Player.Instance.SubtractTurn();
            foreach (MapNode node in MapNodes)
            {
                MapNode.NodeType state = GameManager.Instance.NodeStates[node.name];
                if (state == MapNode.NodeType.Player)
                {
                    PlayerNode = node;
                    Image img = PlayerNode.GetComponent<Image>();
                    img.sprite = Resources.Load<Sprite>("Map_Icon_Player");
                }
                else
                {
                    node.AssignEvent(state);
                }
            }

        }
        else
        {
            if (NumberOfDilemmas + NumberOfGigs != MapNodes.Length - 1)
            {
                Debug.LogError("Number of events does not match number of nodes minus one for player start node.");
                return;
            }
            RandomlyPlacePlayer();
            AssignEvents();

        }

        UpdateAccessibleNodes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        for (int index = 0; index < MapNodes.Length; index++)
        {
            MapNode node = MapNodes[index];
            if (node == PlayerNode)
            {
                GameManager.Instance.NodeStates[node.name] = MapNode.NodeType.Completed;
            }
            else
            {
                if (!GameManager.Instance.NodeStates.ContainsKey(node.name))
                {
                    GameManager.Instance.NodeStates[node.name] = node.nodeType;
                }
                else if (GameManager.Instance.NodeStates[node.name] != MapNode.NodeType.Player)
                {
                    GameManager.Instance.NodeStates[node.name] = node.nodeType;
                }
            }
        }
    }

    private void RandomlyPlacePlayer()
    {
        int randomIndex = Random.Range(0, MapNodes.Length);
        PlayerNode = MapNodes[randomIndex];
        // Get the image component from the game object and set the sprite
        Image img = PlayerNode.GetComponent<Image>();
        img.sprite = Resources.Load<Sprite>("Map_Icon_Player");
    }

    private void AssignEvents()
    {
        MapNode.NodeType[] events = new MapNode.NodeType[NumberOfDilemmas + NumberOfGigs];
        for (int i = 0; i < NumberOfDilemmas; i++)
        {
            events[i] = MapNode.NodeType.Dilemma;
        }
        for (int i = NumberOfDilemmas; i < NumberOfDilemmas + NumberOfGigs; i++)
        {
            events[i] = MapNode.NodeType.Gig;
        }

        // Shuffle the events list
        for (int i = 0; i < events.Length; i++)
        {
            MapNode.NodeType temp = events[i];
            int randomIndex = Random.Range(i, events.Length);
            events[i] = events[randomIndex];
            events[randomIndex] = temp;
        }

        int eventIndex = 0;

        // For each node, assign an event based on its type except for the player's current node
        for (int i = 0; i < MapNodes.Length; i++)
        {
            if (MapNodes[i] != PlayerNode)
            {
                MapNodes[i].AssignEvent(events[eventIndex]);
                eventIndex++;
            }
        }
    }

    private void UpdateAccessibleNodes()
    {
        foreach (MapNode node in MapNodes)
        {
            node.DisableNode();
        }

        foreach (MapNode node in PlayerNode.accessibleNodes)
        {
            node.EnableNode();
        }

        PlayerNode.EnableNode();
    }
}
