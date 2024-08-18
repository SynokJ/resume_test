public class ShootingTimer
{
    public event System.Action OnTimerCompleted = default;

    private float _timerLimit = 0;
    private float _currentTime = 0;

    public ShootingTimer(float timerLimit)
    {
        _timerLimit = timerLimit;
        _currentTime = timerLimit;
    }

    ~ShootingTimer()
    {
    }

    public void UpdateTimer()
    {
        if (_currentTime <= 0)
        {
            _currentTime = _timerLimit;
            OnTimerCompleted?.Invoke();
        }
        else
            _currentTime -= UnityEngine.Time.deltaTime;
    }

    public void UpdateTimeLimit(float lim)
        => _timerLimit = lim;
}
