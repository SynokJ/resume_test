public class BossStageTimer
{
    public float TimerLimit { get => _timerLimit; }
    public float CurrentTimer { get => _currentTime; }

    private float _timerLimit = 0;
    private float _currentTime = 0;

    public event System.Action UpdateStage = default;

    public BossStageTimer(float timeValue)
    {
        _timerLimit = timeValue;
        _currentTime = timeValue;
    }

    public void SubscribeAction(System.Action action)
        => UpdateStage += action;

    public void UnsubscribeAction(System.Action action)
        => UpdateStage -= action;

    public void UpdateTimer()
    {
        if (_currentTime <= 0)
        {
            UpdateStage?.Invoke();
            _currentTime = _timerLimit;
        }
        else
            _currentTime -= UnityEngine.Time.deltaTime;
    }

    public void UpdateTimerLimit(float lim)
        => _timerLimit = lim;
}
