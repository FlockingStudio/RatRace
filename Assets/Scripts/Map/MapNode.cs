using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public enum NodeType { None, Gig, Dilemma }
    public MapNode[] accessibleNodes;
    public NodeType nodeType = NodeType.None;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignEvent(NodeType eventType)
    {
        nodeType = eventType;
        Image img = GetComponent<Image>();
        switch (eventType)
        {
            case NodeType.Gig:
                img.sprite = Resources.Load<Sprite>("Map_Icon_Gig");
                break;
            case NodeType.Dilemma:
                img.sprite = Resources.Load<Sprite>("Map_Icon_Dilemma");
                break;
            default:
                break;
        }
    }

    public void DisableNode()
    {
        Image img = GetComponent<Image>();
        // set the color having a transparency of 25%
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.25f);
        Button btn = GetComponent<Button>();
        btn.interactable = false;
    }

    public void EnableNode()
    {
        Image img = GetComponent<Image>();
        // set the color having a transparency of 100%
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        Button btn = GetComponent<Button>();
        btn.interactable = true;
    }

    public Boolean IsAccessible()
    {
        return GetComponent<Button>().interactable;
    }

    public void OnNodeClicked()
    {
        if (IsAccessible())
        {
            switch (nodeType)
            {
                case NodeType.Gig:
                    GameManager.Instance.OpenGig();
                    break;
                case NodeType.Dilemma:
                    GameManager.Instance.OpenDilemma();
                    break;
                default:
                    Debug.Log("No event assigned to this node.");
                    break;
            }
        }
    }
}
