using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MasterGameStateController : Singleton<MasterGameStateController>
{
    private enum State
    {
        Initialize,
        Restart,
        Playing,
        PostGame,
    }

    public Character_IC[] Character_ICs;

    private StateMachine<State> _StateMachine;

    private void Awake()
    {
        _StateMachine = StateMachine<State>.Initialize(this);
        _StateMachine.Changed += OnStateMachineChanged;
    }

    private void Start()
    {
        _StateMachine.ChangeState(State.Initialize);
    }

    private void OnStateMachineChanged(State state)
    {
        Debug.Log("Changed state to: " + state.ToString());
    }

    public void StartEndSequence()
    {
        _StateMachine.ChangeState(State.PostGame);
    }

    public void StartRestartSequence()
    {
        _StateMachine.ChangeState(State.Restart);
    }

    public void StartConversation(GameInfo.Character character)
    {
        var ic = Character_ICs.FirstOrDefault(e => e.Character.Equals(character));
        ic.StartConversation();
    }

    #region states

    private IEnumerator Initialize_Enter()
    {
        yield return new WaitForEndOfFrame();
        Character_ICs = FindObjectsOfType<Character_IC>();
        _StateMachine.ChangeState(State.Playing);
    }

    private void Restart_Enter()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void Playing_Enter()
    {
        StartConversation(GameInfo.Character.clockhead);
    }

    private void PostGame_Enter()
    {

    }

    #endregion
}
