public class EnemyMovement
{

    private UnityEngine.Rigidbody2D _enemyRB;
    private UnityEngine.Vector2[] _movementDirection = { 
        new UnityEngine.Vector2(0, 1),
        new UnityEngine.Vector2(0.5f, 0.5f),
        new UnityEngine.Vector2(1, 0),
        new UnityEngine.Vector2(0.5f, -0.5f),
        new UnityEngine.Vector2(0, -1),
        new UnityEngine.Vector2(-0.5f, -0.5f),
        new UnityEngine.Vector2(-1, 0),
        new UnityEngine.Vector2(-0.5f, 0.5f)
    };

    public EnemyMovement(UnityEngine.Rigidbody2D enemyRB)
    {
        _enemyRB = enemyRB;
    }

    ~EnemyMovement()
    {
    }

    public void MoveByRandomDirection()
    {
        _enemyRB.velocity = GetRndDirection() * GameConstants.ENEMY_MOVEMENT_SPEED;
    }

    private UnityEngine.Vector2 GetRndDirection()
    {
        int size = _movementDirection.Length;
        int dirId = UnityEngine.Random.Range(0, size);
        return _movementDirection[dirId];
    }
}
