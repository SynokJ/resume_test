using System.Collections.Generic;

public class BossStageSystem
{
    private GameEnumerators.BossStage _currentStage = default;
    private Dictionary<GameEnumerators.BossStage, System.Action> _stageActions =
        new Dictionary<GameEnumerators.BossStage, System.Action>();

    private Dictionary<GameEnumerators.BossStage, System.Action> _stagePreparations =
        new Dictionary<GameEnumerators.BossStage, System.Action>();

    private event System.Action OnFirstStaged = default;
    private event System.Action OnSecondStaged = default;
    private event System.Action OnThirdStaged = default;

    private event System.Action OnFirstPrepared = default;
    private event System.Action OnSecondPrepared = default;
    private event System.Action OnThirdPrepared = default;

    private event System.Action OnCurrentStaged = default;

    public BossStageSystem()
    {

    }

    ~BossStageSystem()
    {

    }

    public void SubscribeStageActions(System.Action[] stages)
    {
        OnFirstStaged += stages[0];
        OnSecondStaged += stages[1];
        OnThirdStaged += stages[2];

        _stageActions[GameEnumerators.BossStage.first] = OnFirstStaged;
        _stageActions[GameEnumerators.BossStage.second] = OnSecondStaged;
        _stageActions[GameEnumerators.BossStage.third] = OnThirdStaged;
    }

    public void UnsubscribeStageActions(System.Action[] stages)
    {
        OnFirstStaged -= stages[0];
        OnSecondStaged -= stages[1];
        OnThirdStaged -= stages[2];

        _stageActions.Clear();
    }

    public void SubscribeStagePrepareActions(System.Action[] stages)
    {
        OnFirstPrepared += stages[0];
        OnSecondPrepared += stages[1];
        OnThirdPrepared += stages[2];

        _stagePreparations[GameEnumerators.BossStage.first] = OnFirstPrepared;
        _stagePreparations[GameEnumerators.BossStage.second] = OnSecondPrepared;
        _stagePreparations[GameEnumerators.BossStage.third] = OnThirdPrepared;
    }

    public void UnsubscribeStagePrepareActions(System.Action[] stages)
    {
        OnFirstPrepared -= stages[0];
        OnSecondPrepared -= stages[1];
        OnThirdPrepared -= stages[2];
     
        _stagePreparations.Clear();
    }

    public void ActivateStageAction()
        => OnCurrentStaged?.Invoke();

    public virtual void SwitchStage()
    {
        _stagePreparations[_currentStage]?.Invoke();
        OnCurrentStaged = _stageActions[_currentStage];
        UnityEngine.Debug.Log(_currentStage.ToString() + " => " + UnityEngine.Time.time);

        if ((int)_currentStage < _stageActions.Count - 1 && _stageActions.ContainsKey(_currentStage))
            _currentStage++;
        else
            _currentStage = 0;
    }
}
