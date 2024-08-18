using UnityEngine;

public class MovingBoss : Boss
{

    private MovingBossAnimation _movingAnimation;

    private EnemyTimer _enemyTimer;
    private BossMovement _movement;
    private BossPathManager _pathGenerator;

    public void InitMovingBoss()
    {
        _enemyTimer = new EnemyTimer(1.0f);

        Animator animator = GetComponent<Animator>();
        _movingAnimation = new MovingBossAnimation(animator);

        _movement = new BossMovement(transform);
        _pathGenerator = new BossPathManager();
    }

    protected override void OnFirstStage()
    {
        OnStageProceeded(GameEnumerators.BossPathType.simpleEdge);
    }

    protected override void OnSecondStage()
    {
        OnStageProceeded(GameEnumerators.BossPathType.crazyDiagonal);
    }

    protected override void OnThirdStage()
    {
        OnStageProceeded(GameEnumerators.BossPathType.crazyEdge);
    }

    private void OnStageProceeded(GameEnumerators.BossPathType type)
    {
        _pathGenerator.UpdatePath(type);
        Vector2 destination = _pathGenerator.GetCurrentDestination();

        _movingAnimation.PlayMoveAnimation(destination - (Vector2)transform.position);
        bool isDestinationReached = _movement.MoveToDestination(destination);
        if (isDestinationReached) _pathGenerator.SwitchToNextStep();

        _enemyTimer.Proceed();
    }

    protected override void OnFirstPrepare()
    {
        // TODO
        _enemyTimer.SetTimerValue(1.0f);
    }

    protected override void OnSecondPrepare()
    {
        // TODO
        _enemyTimer.SetTimerValue(0.75f);
    }

    protected override void OnThirdPrepare()
    {
        // TODO
        _enemyTimer.SetTimerValue(0.5f);
    }
}
