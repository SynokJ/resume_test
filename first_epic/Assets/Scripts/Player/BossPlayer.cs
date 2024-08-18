using Unity.VisualScripting;
using UnityEngine;

public class BossPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Joystick _joystick;
    private PlayerBossMovement _movement;
    private PlayerAnimation _playerAnimation;

    void Awake()
    {
        _joystick = new Joystick();
        _movement = new PlayerBossMovement(this);

        _playerAnimation = new PlayerAnimation(_animator);
    }

    private void OnEnable()
    {
        _joystick.OnJoystickMoving += MovePlayerByJoystick;
        _movement.OnResetAnimation += _playerAnimation.ResetAnimation;
        _movement.OnMoveAnimated += _playerAnimation.AnimateByDirectionType;
    }

    private void OnDisable()
    {
        _joystick.OnJoystickMoving -= MovePlayerByJoystick;
        _movement.OnResetAnimation -= _playerAnimation.ResetAnimation;
        _movement.OnMoveAnimated -= _playerAnimation.AnimateByDirectionType;
    }

    void Update()
    {
        _joystick.UpdateJoystick();
    }

    private void MovePlayerByJoystick()
        => _movement.MoveByDirection(_joystick.System.Direction);

}
