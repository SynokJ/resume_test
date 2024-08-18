using System.Diagnostics;

public class JoystickSystem
{
    public UnityEngine.Vector2 Direction { get => _direction; }

    private Joystick _joystick = null;
    private UnityEngine.Vector2 _originPos = default;
    private UnityEngine.Vector2 _direction = default;

    public JoystickSystem(Joystick joystick)
    {
        _joystick = joystick;
        _joystick.OnJoystickActivated += InitOriginPos;
        _joystick.OnJoystickMoving += InitDirection;
    }

    ~JoystickSystem()
    {
        _originPos = default;
        _joystick.OnJoystickActivated -= InitOriginPos;
        _joystick.OnJoystickMoving -= InitDirection;
    }

    private void InitDirection()
    {
        UnityEngine.Vector2 touchPos = UnityEngine.Input.mousePosition;
        _direction = (touchPos - _originPos).normalized;
    }

    private void InitOriginPos()
        => _originPos = UnityEngine.Input.mousePosition;
}
