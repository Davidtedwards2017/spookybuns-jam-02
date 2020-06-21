using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;
using System.Linq;
using Data;

public class MasterGameStateController : Singleton<MasterGameStateController>
{
    private enum State
    {
        Initialize,
        Title,
        Restart,
        Playing,
        PostGame,
    }

    public List<SoundEffectData> PlayerTypingSounds;

    public Module TitleScreenUi;
    public Module FadeUi;
    public GameInfo.Character StartingConversationCharacter;

    public Character_IC[] Character_ICs;
    private StateMachine<State> _StateMachine;

    private void Awake()
    {
        _StateMachine = StateMachine<State>.Initialize(this);
        _StateMachine.Changed += OnStateMachineChanged;
    }

    private void Start()
    {
        ConversationController.Instance.SubscribeToEvent("game.restart", StartRestartSequence);
        ConversationController.Instance.SubscribeToEvent("game.end", StartEndSequence);

        _StateMachine.ChangeState(State.Initialize);

        NavigationUiController.Instance.SetActive(false);
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
        _StateMachine.ChangeState(State.Title);
    }

    private IEnumerator Title_Enter()
    {
        FadeUi.SetActive(false);
        TitleScreenUi.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Input.anyKeyDown);
        FadeUi.SetActive(true);
        TitleScreenUi.SetActive(false);
        yield return new WaitForSeconds(1.1f);
        FadeUi.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        _StateMachine.ChangeState(State.Playing);
    }


    private void Restart_Enter()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void Playing_Enter()
    {
        StartConversation(StartingConversationCharacter);
    }

    private IEnumerator PostGame_Enter()
    {
        FadeUi.SetActive(true);
        yield return new WaitForSeconds(1.0f);
 
        yield return EndSummaryUiController.Instance.StartEndGameSummary(CacluateReincarnationResult());
        _StateMachine.ChangeState(State.Restart);
    }

    private ReincarnationAsset CacluateReincarnationResult()
    {
        return ReincarnationAssetLoader.Instance.Reincarnations
            .FirstOrDefault(e => ConditionParser.ProcessConditionalStatement(e.Condition));
    }

    #endregion
}
