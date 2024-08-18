public class EnemyChase
{
    public event System.Action<UnityEngine.Vector2> OnChaseAnimated = default;

    private UnityEngine.Transform _targetTr;
    private UnityEngine.Transform _currentTr;

    private float _currentChargeTime = 0;
    private float _currentChasingSpeed = 
        GameConstants.ENEMY_CHASING_SPEED_MIN;

    public EnemyChase(UnityEngine.Transform currentTr)
    {
        _currentTr = currentTr;
    }

    ~EnemyChase()
    {
    }

    public void MoveToTarget()
    {
        if (_targetTr == null) return;
        _currentChargeTime += UnityEngine.Time.deltaTime;
        _currentTr.position = GetDestinationPosition();
    }

    public bool IsTargetVisible(UnityEngine.Transform targetTr)
    {
        UnityEngine.RaycastHit2D[] hits = 
            UnityEngine.Physics2D.LinecastAll(_currentTr.position, targetTr.position);

        int size = hits.Length;
        for (int i = 0; i < size; ++i)
            if (hits[i].transform.tag.Equals("Wall"))
                return false;
        return true;
    }

    private UnityEngine.Vector2 GetDestinationPosition()
    {
        float interpoltion = _currentChargeTime / GameConstants.ENEMY_CHASING_TIMER_VALUE;
        _currentChasingSpeed = UnityEngine.Mathf.Lerp(_currentChasingSpeed,
            GameConstants.ENEMY_CHASING_SPEED_MAX, interpoltion);

        UnityEngine.Vector2 dir = (_targetTr.position - _currentTr.position).normalized;
        OnChaseAnimated?.Invoke(dir);

        return UnityEngine.Vector2.MoveTowards(_currentTr.position, _targetTr.position,
               _currentChasingSpeed * UnityEngine.Time.deltaTime);
    }

    public void SetTarget(UnityEngine.Transform targetTr)
    {
        _targetTr = targetTr;
        _currentChasingSpeed = GameConstants.ENEMY_CHASING_SPEED_MIN;
    }

    public void ResetTarget()
    {
         _targetTr = null;
        _currentChargeTime = 0.0f;
    }
}
