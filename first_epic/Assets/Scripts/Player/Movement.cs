public class Movement
{

    private UnityEngine.Rigidbody2D _characterRB;

    public Movement(UnityEngine.Rigidbody2D characterRB)
    {
        _characterRB = characterRB;
    }

    ~Movement()
    {
        _characterRB = null;
    }

    public void MoveByDirection(UnityEngine.Vector2 dir)
        => _characterRB.velocity = dir * GameConstants.PLAYER_MOVEMENT_SPEED;

    public void StopMovement()
        => _characterRB.velocity = UnityEngine.Vector2.zero;
}
