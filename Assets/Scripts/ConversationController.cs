using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class ConversationController : Singleton<ConversationController>
{
    public enum State
    {
        initializing,
        actordialogue,
    }
    public ConversationData conversation;
    private ConversationData.DialogueData currentDialogue;

    public ConversationModule ConversationModule;

    public StateMachine<State> _StateMachine;

    private void Awake()
    {
        _StateMachine = StateMachine<State>.Initialize(this);
        _StateMachine.ChangeState(State.initializing);
    }


    private void initializing_enter()
    {

    }
    
    private void actordialogue_enter()
    {
        currentDialogue = conversation.Dialogues[0];
    }
}