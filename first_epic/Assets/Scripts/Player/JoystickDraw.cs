using System.Diagnostics;

public class JoystickDraw
{
    private Joystick _joystick = null;
    private UnityEngine.RectTransform _innerCircle;
    private UnityEngine.RectTransform _outerCircle;

    private UnityEngine.UI.Image _outerCircelImage;
    private UnityEngine.UI.Image _innerCircelImage;

    private float _distLimit = 0;

    public JoystickDraw(UnityEngine.GameObject innerCircle, UnityEngine.GameObject outterCircle)
    {
        _innerCircle = innerCircle.GetComponent<UnityEngine.RectTransform>();
        _outerCircle = outterCircle.GetComponent<UnityEngine.RectTransform>();

        _outerCircelImage = outterCircle.GetComponent<UnityEngine.UI.Image>();
        _innerCircelImage = innerCircle.GetComponent<UnityEngine.UI.Image>();

        _distLimit = _outerCircle.rect.width * 0.5f;
    }

    ~JoystickDraw()
    {
        _joystick = null;

        _joystick.OnJoystickActivated -= ShowJoystick;
        _joystick.OnJoystickDeactivated -= HideJoystick;
        _joystick.OnJoystickMoving -= MoveInnerCircle;
    }

    public void InitJoystick(Joystick joystick)
    {
        _joystick = joystick;

        _joystick.OnJoystickActivated += ShowJoystick;
        _joystick.OnJoystickDeactivated += HideJoystick;
        _joystick.OnJoystickMoving += MoveInnerCircle;
    }

    private void HideJoystick()
    {
        _innerCircelImage.enabled = false;
        _outerCircelImage.enabled = false;
    }

    private void ShowJoystick()
    {
        _innerCircle.position = UnityEngine.Input.mousePosition;
        _outerCircle.position = UnityEngine.Input.mousePosition;

        _innerCircelImage.enabled = true;
        _outerCircelImage.enabled = true;
    }

    private void MoveInnerCircle()
    {
        UnityEngine.Vector2 tempPos = _outerCircle.position + (UnityEngine.Vector3)_joystick.System.Direction * _distLimit * 0.25f;
        float dist = UnityEngine.Vector2.Distance(tempPos, _outerCircle.position);
        if (dist < _distLimit) _innerCircle.position = tempPos;
    }
}
