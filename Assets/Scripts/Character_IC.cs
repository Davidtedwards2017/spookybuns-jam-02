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
    public Vector3 HoverScaleAmount = new Vector3(1.05f, 1.05f, 1.0f);
    public Vector3 DefaultScale = Vector3.one;
    public float ConversationMusicPitch = 1;

    public List<SoundEffectData> TypingSounds;

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
        if (!ConversationController.Instance.InConversation())
        {
            StartConversation();
        }
    }

    public void OnMouseOver()
    {
        if(!ConversationController.Instance.InConversation())
        {
            _Renderer.transform.DOScale(HoverScaleAmount, 0.1f);
        }
    }

    public void OnMouseExit()
    {
        if (!ConversationController.Instance.InConversation())
        {
            _Renderer.transform.DOScale(DefaultScale, 0.1f);
        }
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
