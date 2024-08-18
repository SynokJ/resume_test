using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D _rb;
    private Animator _animator;
    private EnemyMovement _movement;
    private EnemyTimer _enemyTimer;
    private EnemyChase _chase;
    private CircleCollider2D _circleCollider;
    private EnemyAnimator _enemyAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        MazePlayer player = collision.gameObject.GetComponent<MazePlayer>();
        if (player != null)
        {
            if (_chase.IsTargetVisible(player.transform))
            {
                player.StartChase(this);
                StartChasing(player.transform);
            }
            else
            {
                player.EndChase(this);
                EndChasing();
            }
        }
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();

        _movement = new EnemyMovement(_rb);
        _chase = new EnemyChase(transform);
        _enemyAnimator = new EnemyAnimator(_animator);
        _enemyTimer = new EnemyTimer(GameConstants.ENEMY_TIMER_VALUE);
    }

    private void OnEnable()
    {
        _enemyTimer.OnTimerCompleted += _movement.MoveByRandomDirection;
        _chase.OnChaseAnimated += _enemyAnimator.SetAnimationByDirection;
    }

    private void OnDisable()
    {
        _enemyTimer.OnTimerCompleted -= _movement.MoveByRandomDirection;
        _chase.OnChaseAnimated += _enemyAnimator.SetAnimationByDirection;
    }

    private void Update()
    {
        _enemyTimer.Proceed();
        _chase.MoveToTarget();
        _enemyAnimator.SetAnimationByDirection(_rb.velocity);
    }

    private void StartChasing(Transform targetTr)
    {
        _enemyTimer.StopTimer();
        _chase.SetTarget(targetTr);

        _circleCollider.radius = GameConstants.ENEMY_TRIGGER_RADIUS_CHASE;
    }

    private void EndChasing()
    {
        _enemyTimer.PlayTimer();
        _chase.ResetTarget();

        _circleCollider.radius = GameConstants.ENEMY_TRIGGER_RADIUS_ORIGIN;
    }
}
