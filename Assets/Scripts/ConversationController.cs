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

    public ConversationModule ConversationModule;

    public DialogueData conversation;
    private DialogueEntry _CurrentDialogue;
    private DialogueEntry _PrevDialogue;

    private ChoiceDialogueOption _SelectedOption;
    public StateMachine<State> _StateMachine;

    private void Awake()
    {
        _StateMachine = StateMachine<State>.Initialize(this);
    }

    private void Start()
    {    
        _StateMachine.ChangeState(State.LoadingConversation);
    }

    public void ChoiceOptionSelected(ChoiceDialogueOption selected)
    {
        _SelectedOption = selected;
    }

    private void LoadingConversation_Enter()
    {
        conversation = ConversationLoader.Instance.ConversationData.First();
        LoadDialogueEntry(conversation.dialogues.First());
        _StateMachine.ChangeState(State.ActorDialogue);
    }

    private void LoadDialogueEntry(string id)
    {
        LoadDialogueEntry(conversation.dialogues.FirstOrDefault(e => e.Id.Equals(id)));
    }

    private void LoadDialogueEntry(DialogueEntry dialogue)
    {
        _CurrentDialogue = dialogue;
    }

    private IEnumerator ActorDialogue_Enter()
    {
        if(_CurrentDialogue != null)
        {
            yield return ConversationModule.PerformDialogue(_CurrentDialogue);
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

            if (_PrevDialogue is BasicDialogueEntry)
            {
                ProcessPost(((BasicDialogueEntry)_PrevDialogue).Post);
            }
            else if(_PrevDialogue is ChoiceDialogueEntry && _SelectedOption != null)
            {
                ProcessPost(_SelectedOption.Post);
                _SelectedOption = null;
            }
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

    }
}