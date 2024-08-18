public class PlayerAnimation
{

    private const string _MOVE_RIGHT_NAME = "move_right";
    private const string _MOVE_LEFT_NAME = "move_left";

    private UnityEngine.Animator _animator;

    public PlayerAnimation(UnityEngine.Animator animator)
    {
        _animator = animator;
    }

    ~PlayerAnimation()
    {
    }

    public void AnimateByDirection(UnityEngine.Vector2 dir)
    {
        ResetAnimation();

        if (dir.x < 0)
            SetMoveLeftAnimation();
        else if (dir.x > 0)
            SetMoveRightAnimation();
    }

    public void AnimateByDirectionType(GameEnumerators.BossPlayerMovementType type)
    {
        if(type == GameEnumerators.BossPlayerMovementType.left)
            SetMoveLeftAnimation();
        else 
            SetMoveRightAnimation();
    }

    public void ResetAnimation()
    {
        _animator.SetBool(_MOVE_RIGHT_NAME, false);
        _animator.SetBool(_MOVE_LEFT_NAME, false);
    }

    private void SetMoveRightAnimation()
        => _animator.SetBool(_MOVE_RIGHT_NAME, true);

    private void SetMoveLeftAnimation()
        => _animator.SetBool(_MOVE_LEFT_NAME, true);
}
