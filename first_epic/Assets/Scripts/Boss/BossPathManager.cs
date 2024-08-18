using System.Collections.Generic;

public class BossPathManager
{
    private int _stepId = 0;
    private List<UnityEngine.Vector2> _currentPath = 
        new List<UnityEngine.Vector2>();
    
    private GameEnumerators.BossPathType _currentType;

    public BossPathManager()
    {
    }

    ~BossPathManager()
    {
        _currentPath.Clear();
    }

    public UnityEngine.Vector2 GetCurrentDestination()
    {
        if (_stepId >= _currentPath.Count)
        {
            _stepId = 0;
            return _currentPath[_stepId];
        }

        return _currentPath[_stepId];
    }
    public void SwitchToNextStep()
    {
        if (_stepId + 1 < _currentPath.Count)
            _stepId++;
    }

    public void UpdatePath(GameEnumerators.BossPathType type)
    {
        if (_currentType == type) return;

        _currentPath = GameConstants.BOSS_PATH_TYPE[type];
        _currentType = type;
        _stepId = 0;
    }
}
