using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_IC : MonoBehaviour
{
    public GameInfo.Character Character;
    public Collider2D Collider;

    public Transform ConversationCameraAnchor;
    public Transform ConversationDialogueBubbleAnchor;
    public float ConversationCameraOrthoSize = 3;

    public DialogueModule DialogueModule;

    public void OnMouseDown()
    {
        StartConversation();
    }

    public void StartConversation()
    {
        ConversationController.Instance.StartConversation(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        DialogueModule = GetComponentInChildren<DialogueModule>();
    }
}
