using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character_IC : MonoBehaviour
{
    public GameInfo.Character Character;
    public Collider2D Collider;
    public MeshRenderer Renderer;

    public Transform ConversationCameraAnchor;
    public Transform ConversationDialogueBubbleAnchor;
    public float ConversationCameraOrthoSize = 3;

    public DialogueModule DialogueModule;
    
    // Start is called before the first frame update
    void Start()
    {
        DialogueModule = GetComponentInChildren<DialogueModule>();
        Renderer = GetComponentInChildren<MeshRenderer>();

        ConversationController.Instance.SubscribeToEvent("startconversation." + Character.ToString(), StartConversation);
        ConversationController.Instance.SubscribeToEvent("hide." + Character.ToString(), Hide);
        ConversationController.Instance.SubscribeToEvent("show."+ Character.ToString(), Show);
    }

    public void OnMouseDown()
    {
        StartConversation();
    }

    public void StartConversation()
    {
        ConversationController.Instance.StartConversation(this);
    }

    public void Hide()
    {
        Collider.enabled = false;
        Renderer.enabled = false;
    }

    public void Show()
    {
        Collider.enabled = true;
        Renderer.enabled = true;
    }
}
