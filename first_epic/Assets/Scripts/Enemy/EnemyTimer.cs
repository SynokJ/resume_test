public class EnemyTimer
{
    public event System.Action OnTimerCompleted = default;

    private float _timerValue;
    private float _currentTime;

    private bool _isRunning = false;

    public EnemyTimer(float timerValue)
    {
        _timerValue = timerValue;
        _isRunning = true;
    }

    ~EnemyTimer()
    {
    }

    public void Proceed()
    {
        if (!_isRunning) return;

        if (_currentTime < 0)
        {
            OnTimerCompleted?.Invoke();
            _currentTime = _timerValue;
        }
        else
            _currentTime -= UnityEngine.Time.deltaTime;
    }

    public void StopTimer()
    {
        _isRunning = false;
        _currentTime = _timerValue;
    }

    public void PlayTimer()
        => _isRunning = true;

    public void SetTimerValue(float time)
    {
        _timerValue = time;
        _currentTime = time;
    }
}
