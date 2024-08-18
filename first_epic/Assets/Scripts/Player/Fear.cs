public class Fear
{
    private UnityEngine.Transform _playerTr;

    private System.Collections.Generic.List<UnityEngine.Transform> _enemies =
        new System.Collections.Generic.List<UnityEngine.Transform>();

    public Fear(UnityEngine.Transform playerTr)
    {
        _playerTr = playerTr;
    }

    ~Fear()
    {
    }

    public void Show()
    {
        if (_enemies.Count == 0) return;
        float minDistance = GetNearestEnemy();
        UnityEngine.Debug.Log(GetFearByDistance(minDistance));
    }

    private string GetFearByDistance(float distance)
    {
        float fearPrec = distance / GameConstants.ENEMY_TRIGGER_RADIUS_CHASE;
        if (fearPrec > 0.6f)
            return "Easy";
        else if (fearPrec > 0.3f)
            return "Norm";
        return "GG";
    }

    private float GetNearestEnemy()
    {
        float resultDistance = float.MaxValue;
        int size = _enemies.Count;
        for (int i = 0; i < size; ++i)
        {
            UnityEngine.Vector2 enemyPos = _enemies[i].position;
            UnityEngine.Vector2 playerPos = _playerTr.position;
            float tempDist = UnityEngine.Vector2.Distance(playerPos, enemyPos);
            if(tempDist < resultDistance)
                resultDistance = tempDist;
        }
        return resultDistance;
    }

    public void EnableFear(UnityEngine.Transform enemyTr)
    {
        if (!_enemies.Contains(enemyTr))
            _enemies.Add(enemyTr);
    }

    public void DisableFear(UnityEngine.Transform enemyTr)
    {
        if (_enemies.Contains(enemyTr))
            _enemies.Remove(enemyTr);
    }
}
