using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character_IC : MonoBehaviour
{
    private Collider2D _Collider;
    private MeshRenderer _Renderer;
    private Animator _Animator;

    public GameInfo.Character Character;

    public Transform ConversationCameraAnchor;
    public Transform ConversationDialogueBubbleAnchor;
    public float ConversationCameraOrthoSize = 3;

    public DialogueModule DialogueModule;
    
    // Start is called before the first frame update
    void Start()
    {
        _Collider = GetComponentInChildren<Collider2D>();
        _Animator = GetComponentInChildren<Animator>();
        _Renderer = GetComponentInChildren<MeshRenderer>();

        DialogueModule = GetComponentInChildren<DialogueModule>();

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

    public void UpdateExpression(GameInfo.Expression expression)
    {
        switch (expression)
        {
            case GameInfo.Expression.angry:
                _Animator.SetTrigger("angry");
                break;
            case GameInfo.Expression.happy:
                _Animator.SetTrigger("happy");
                break;
            case GameInfo.Expression.neutral:
                _Animator.SetTrigger("neutral");
                break;
        }
    }

    public void Hide()
    {
        _Collider.enabled = false;
        _Renderer.enabled = false;
    }

    public void Show()
    {
        _Collider.enabled = true;
        _Renderer.enabled = true;
    }
}
