using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MonsterLove.StateMachine;
using Data;
using System.Text.RegularExpressions;

public class ConversationController : Singleton<ConversationController>
{
    public enum State
    {
        LoadingConversation,
        ActorDialogue,
        PostDialogue,
        EndConversation,
    }

    public GameInfo.Character ConversationWithCharacter;
    public DialogueData conversation;

    private Node _CurrentDialogue;
    private Node _PrevDialogue;

    private ChoiceDialogueOption _SelectedOption;
    public StateMachine<State> _StateMachine;

    private void Awake()
    {
        _StateMachine = StateMachine<State>.Initialize(this);
    }

    private void Start()
    {    
        //_StateMachine.ChangeState(State.LoadingConversation);
    }

    public void StartConversation(GameInfo.Character character)
    {
        ConversationWithCharacter = character;
        _StateMachine.ChangeState(State.LoadingConversation);
    }

    public void ChoiceOptionSelected(ChoiceDialogueOption selected)
    {
        _SelectedOption = selected;
    }

    private void LoadingConversation_Enter()
    {
        conversation = ConversationLoader.Instance.ConversationData.First();
        LoadDialogueEntry(conversation.nodes.First());
        _StateMachine.ChangeState(State.ActorDialogue);
    }

    private void LoadDialogueEntry(string id)
    {
        LoadDialogueEntry(conversation.nodes.FirstOrDefault(e => e.Id.Equals(id)));
    }

    private void LoadDialogueEntry(Node dialogue)
    {
        _CurrentDialogue = dialogue;
    }

    private IEnumerator ActorDialogue_Enter()
    { 
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
        if (_CurrentDialogue != null)
        {
            _PrevDialogue = _CurrentDialogue;
            _CurrentDialogue = null;

            ProcessNode(_PrevDialogue);
        }

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
            }
        }
    }

    private void ProcessConditional(Conditional conditional)
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

    private bool ProcessConditionalStatement(string statmentText)
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

    private void ContinueDialogue()
    {
        _StateMachine.ChangeState(State.ActorDialogue);
    }

    private void EndConversation_Enter()
    {
        ConversationUiController.Instance.SetActive(false);
        NavigationUiController.Instance.SetActive(true);
    }
}