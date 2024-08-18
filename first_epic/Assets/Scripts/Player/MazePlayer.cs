using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    private GameObject _innerCircle;
    private GameObject _outterCircle;
    private Gameplay _gameplay;
    
    private Rigidbody2D _playerRB;
    private Animator _playerAnimator;

    private Fear _fear;
    private Joystick _joystick;
    private Movement _movement;
    private JoystickDraw _joystickDraw;
    private PlayerAnimation _playerAnimation;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) _gameplay.LoseGame();
    }

    public void InitMazePlayer(GameObject inner, GameObject outter, Gameplay game)
    {
        _innerCircle = inner;
        _outterCircle = outter;
        _gameplay = game;
    }


    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _joystick = new Joystick();
        _fear = new Fear(transform);
        _movement = new Movement(_playerRB);
        _joystickDraw = new JoystickDraw(_innerCircle, _outterCircle);
        _joystickDraw.InitJoystick(_joystick);

        _playerAnimation = new PlayerAnimation(_playerAnimator);


        _joystick.OnJoystickDeactivated += _movement.StopMovement;
        _joystick.OnJoystickDeactivated += _playerAnimation.ResetAnimation;

        _joystick.OnJoystickMoving += MovePlayerByJoystick;
        _joystick.OnJoystickMoving += AnimatePlyerByJoystick;
    }

    private void OnDestroy()
    {
        _joystick.OnJoystickDeactivated -= _movement.StopMovement;
        _joystick.OnJoystickDeactivated -= _playerAnimation.ResetAnimation;

        _joystick.OnJoystickMoving -= MovePlayerByJoystick;
        _joystick.OnJoystickMoving -= AnimatePlyerByJoystick;
    }

    void Update()
    {
        _joystick.UpdateJoystick();
        _fear.Show();
    }

    private void MovePlayerByJoystick()
        => _movement.MoveByDirection(_joystick.System.Direction);

    private void AnimatePlyerByJoystick()
    {
        _playerAnimation.AnimateByDirection(_joystick.System.Direction);
        Debug.Log(_joystick.System.Direction + " => " + Time.time);
    }

    public void StartChase(Enemy enemy)
        => _fear.EnableFear(enemy.transform);

    public void EndChase(Enemy enemy)
        => _fear.DisableFear(enemy.transform);
}
