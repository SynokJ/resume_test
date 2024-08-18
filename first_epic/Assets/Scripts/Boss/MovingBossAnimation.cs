using System.Collections.Generic;

public class MovingBossAnimation
{
    private const string _ANIMATION_MOVING_FORWARD_NAME = "on_forward";
    private const string _ANIMATION_MOVING_BACKWARD_NAME = "on_back";
    private const string _ANIMATION_MOVING_LEFT_NAME = "on_left";
    private const string _ANIMATION_MOVING_RIGHT_NAME = "on_right";

    private readonly Dictionary<DirectionType, string> _animNamesByDirectionType =
        new Dictionary<DirectionType, string>()
        {
            { DirectionType.forward, _ANIMATION_MOVING_FORWARD_NAME },
            { DirectionType.backward, _ANIMATION_MOVING_BACKWARD_NAME},
            { DirectionType.right, _ANIMATION_MOVING_RIGHT_NAME},
            { DirectionType.left, _ANIMATION_MOVING_LEFT_NAME },
        };

    private UnityEngine.Animator _animator;

    public MovingBossAnimation(UnityEngine.Animator animator)
    {
        _animator = animator;
    }

    public void PlayMoveAnimation(UnityEngine.Vector2 destination)
    {
        ResetAnimations();
        DirectionType type = DetectDirectionType(destination);
        UnityEngine.Debug.Log($"type: {type} dest: {destination.ToString()}");
        _animator.SetBool(_animNamesByDirectionType[type], true);
    }

    private void ResetAnimations()
    {
        _animator.SetBool(_ANIMATION_MOVING_FORWARD_NAME, false);
        _animator.SetBool(_ANIMATION_MOVING_BACKWARD_NAME, false);
        _animator.SetBool(_ANIMATION_MOVING_RIGHT_NAME, false);
        _animator.SetBool(_ANIMATION_MOVING_LEFT_NAME, false);
    }

    private DirectionType DetectDirectionType(UnityEngine.Vector2 destination)
    {
        DirectionType result = default;
        float maxX = System.MathF.Abs(destination.x);
        float maxY = System.MathF.Abs(destination.y);
        if (maxX > maxY)
            result = destination.x > 0 ? DirectionType.right : DirectionType.left;
        else
            result = destination.y > 0 ? DirectionType.backward : DirectionType.forward;
        return result;
    }
}

public enum DirectionType
{
    forward = 0,
    backward = 1,
    left = 2,
    right = 3
}
