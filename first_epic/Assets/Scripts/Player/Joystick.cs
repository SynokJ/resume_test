using System.Diagnostics;

public class Joystick
{
    public JoystickSystem System { get => _system; }

    public event System.Action OnJoystickDeactivated = default;
    public event System.Action OnJoystickActivated = default;
    public event System.Action OnJoystickMoving = default;

    private JoystickSystem _system;
    private bool _isActivated = false;

    public Joystick()
    {
        _system = new JoystickSystem(this);
        OnJoystickDeactivated += DeactivateJoystick;
    }

    ~Joystick()
    {
        _system = null;
        OnJoystickDeactivated -= DeactivateJoystick;
    }

    public void UpdateJoystick()
    {
        ActivateJoystick();

        if (UnityEngine.Input.GetMouseButtonUp(0) && _isActivated)
            OnJoystickDeactivated?.Invoke();

        if (!_isActivated) return;
        OnJoystickMoving?.Invoke();
    }

    private void ActivateJoystick()
    {
        if (_isActivated)
            return;

        _isActivated = !IsTouchingUI() && UnityEngine.Input.GetMouseButtonDown(0);
        if (_isActivated) OnJoystickActivated?.Invoke();
    }

    private bool IsTouchingUI()
    {
        UnityEngine.EventSystems.EventSystem currentEvent = UnityEngine.EventSystems.EventSystem.current;
        return currentEvent.IsPointerOverGameObject();
    }

    private void DeactivateJoystick()
        => _isActivated = false;
}
