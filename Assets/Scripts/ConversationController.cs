using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MonsterLove.StateMachine;
using Data;
using DG.Tweening;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using Utilites;

public class ConversationController : Singleton<ConversationController>
{
    public enum State
    {
        LoadingConversation,
        ActorDialogue,
        PostDialogue,
        EndConversation,
    }

    public Camera sceneCamera;
    public GameInfo.Character ConversationWithCharacter;
    public DialogueData conversation;

    private Node _CurrentDialogue;
    private Node _PrevDialogue;

    private ChoiceDialogueOption _SelectedOption;
    public StateMachine<State> _StateMachine;

    public float convoCamaraUpdateDuration = 0.5f;
    private Vector3 prevSceneCameraPos;
    private Vector3 convoSceneCameraPos;
    private float prevSceneCameraOrthoSize;
    private float convoSceneCameraOrthoSize;
    private Sequence cameraUpdateSequence;

    public Vector3 ConvoDialoguePos { get; private set; }
    public Transform ConvoDialogueTransform { get; private set; }

    private Dictionary<string, UnityEvent> _Events = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
        _StateMachine = StateMachine<State>.Initialize(this);
    }

    public void StartConversation(Character_IC ic)
    {
        ConvoDialoguePos = ic.ConversationDialogueBubbleAnchor.position;
        ConvoDialogueTransform = ic.ConversationDialogueBubbleAnchor;

        prevSceneCameraPos = sceneCamera.transform.position;
        prevSceneCameraOrthoSize = sceneCamera.orthographicSize;

        convoSceneCameraPos = ic.ConversationCameraAnchor.position;
        convoSceneCameraOrthoSize = ic.ConversationCameraOrthoSize;

        ConversationWithCharacter = ic.Character;
        _StateMachine.ChangeState(State.LoadingConversation);
    }

    public void ChoiceOptionSelected(ChoiceDialogueOption selected)
    {
        _SelectedOption = selected;
    }

    private void LoadingConversation_Enter()
    {
        conversation = ConversationLoader.Instance.GetCharacterConversation(ConversationWithCharacter);
        LoadDialogueEntry(conversation.nodes.First());
        _StateMachine.ChangeState(State.ActorDialogue);
    }

    private void LoadDialogueEntry(string id)
    {
        var node = conversation.nodes.FirstOrDefault(e => e.Id.Equals(id));
        if (node == null)
        {
            Debug.LogError("cannot find node with id:" + id);
        }
        else
        {
            LoadDialogueEntry(node);
        }
    }

    private void LoadDialogueEntry(Node dialogue)
    {
        _CurrentDialogue = dialogue;
    }

    public void UpdateCamera(Vector3 pos, float orthoSize, float duration)
    {
        if(cameraUpdateSequence != null && cameraUpdateSequence.IsPlaying())
        {
            cameraUpdateSequence.Kill();
        }

        cameraUpdateSequence = DOTween.Sequence();
        cameraUpdateSequence.Join(sceneCamera.DOOrthoSize(orthoSize, duration));
        cameraUpdateSequence.Join(sceneCamera.transform.DOMove(pos, duration));
        cameraUpdateSequence.Play();
    }

    private IEnumerator ActorDialogue_Enter()
    {
        UpdateCamera(convoSceneCameraPos, convoSceneCameraOrthoSize, convoCamaraUpdateDuration);

        ConversationUiController.Instance.SetActive(true);
        NavigationUiController.Instance.SetActive(false);

        if (_CurrentDialogue != null)
        {
            yield return ConversationUiController.Instance.PerformDialogue(_CurrentDialogue);
        }

        _StateMachine.ChangeState(State.PostDialogue);
    }

    private void ActorDialogue_Update()
    {

    }

    private void ActorDialogue_Exit()
    {
    }

    private void PostDialogue_Enter()
    {
        //if (_CurrentDialogue != null)
        //{
            _PrevDialogue = _CurrentDialogue;
            _CurrentDialogue = null;

            ProcessNode(_PrevDialogue);
        //}

        if (_CurrentDialogue != null)
        {
            _StateMachine.ChangeState(State.ActorDialogue);
        }
        else
        {
            _StateMachine.ChangeState(State.EndConversation);
        }
    }

    private void ProcessNode(Node node)
    {
        if (node == null) return;

        if (node is BasicDialogueEntry)
        {
            ProcessPost(((BasicDialogueEntry)node).Post);
        }
        else if (node is ChoiceDialogueEntry && _SelectedOption != null)
        {
            ProcessPost(_SelectedOption.Post);
            _SelectedOption = null;
        }
        else if (node is Conditional)
        {
            ProcessConditional((Conditional)node);
        }
    }

    private void ProcessPost(string[] postEntries)
    {
        foreach (var postEntry in postEntries)
        {
            var split = postEntry.Split(':');
            switch (split[0])
            {
                case "next":
                    LoadDialogueEntry(split[1]);
                    break;
                case "flag":
                    ProcessFlag(split[1]);
                    break;
                case "event":
                    TriggerEvent(split[1]);
                    break;
            }
        }
    }

    public void ProcessConditional(Conditional conditional)
    {
        if (ProcessConditionalStatement(conditional.Condition))
        {
            ProcessPost(conditional.If);
        }
        else
        {
            ProcessPost(conditional.Else);
        }
    }

    public static bool ProcessConditionalStatement(string statmentText)
    {
        var split = Regex.Split(statmentText, @"([=><])");
        string symbol = split[1];
        int conditionalValue = int.Parse(split[2]);
        int value = StatsController.Instance.GetFlagValue(split[0]);

        switch (symbol)
        {
            case "=":
                return value == conditionalValue;
            case ">":
                return value > conditionalValue;
            case "<":
                return value < conditionalValue;
        }

        return false;
    }

    private void ProcessFlag(string flagText)
    {
        var split = Regex.Split(flagText, @"([=\+\-])");
        string flagname = split[0];
        string symbol = split[1];
        int value = int.Parse(split[2]);

        switch (symbol)
        {
            case "=":
                StatsController.Instance.SetFlagValue(flagname, value);
                break;
            case "+":
                StatsController.Instance.AdjustFlagValue(flagname, value);
                break;
            case "-":
                StatsController.Instance.AdjustFlagValue(flagname, -value);
                break;
        }
    }

    public void SubscribeToEvent(string eventName, UnityAction action)
    {
        var unityEvent = _Events.SafeGetOrInitialize(eventName);
        unityEvent.AddListener(action);
    }

    private void TriggerEvent(string eventName)
    {
        var unityEvent = _Events.SafeGet(eventName);
        if(unityEvent != null)
        {
            unityEvent.Invoke();
        }
    }

    private void ContinueDialogue()
    {
        _StateMachine.ChangeState(State.ActorDialogue);
    }

    private void EndConversation_Enter()
    {
        UpdateCamera(prevSceneCameraPos, prevSceneCameraOrthoSize, convoCamaraUpdateDuration);
        ConversationUiController.Instance.SetActive(false);
        NavigationUiController.Instance.SetActive(true);
    }
}