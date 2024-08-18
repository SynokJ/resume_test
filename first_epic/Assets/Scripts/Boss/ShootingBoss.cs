using UnityEngine;

public class ShootingBoss : Boss
{
    private ShootingBossAnimation _bossAnimation;

    private GameObject _bulletPref;
    private Transform _playerTr;

    private ShootingSystem _shooting;
    private ShootingTimer _shootingTimer;

    public void InitShootingBoss(GameObject bulletPref, Transform playerTr)
    {
        _bulletPref = bulletPref;
        _playerTr = playerTr;

        transform.position = Vector2.zero;
        _shooting = new ShootingSystem(_bulletPref, transform);
        
        Animator anim = GetComponent<Animator>();
        _bossAnimation = new ShootingBossAnimation(anim);

        _shootingTimer = new ShootingTimer(1.0f);
        _shootingTimer.OnTimerCompleted += Shoot;
        _shootingTimer.OnTimerCompleted += _bossAnimation.PlayShootAnimation;
    }

    private void Shoot()
    {
        Vector2 dir = (_playerTr.transform.position - transform.position).normalized;
        _shooting.ShootByDirection(dir);
    }

    protected override void OnFirstStage()
    {
        _shootingTimer.UpdateTimer();
    }

    protected override void OnSecondStage()
    {
        _shootingTimer.UpdateTimer();
    }

    protected override void OnThirdStage()
    {
        _shootingTimer.UpdateTimer();
    }

    protected override void OnFirstPrepare()
    {
        _shootingTimer.UpdateTimeLimit(1.0f);
        _timer.UpdateTimerLimit(10.0f);
    }

    protected override void OnSecondPrepare()
    {
        _shootingTimer.UpdateTimeLimit(0.5f);
        _timer.UpdateTimerLimit(15.0f);
    }

    protected override void OnThirdPrepare()
    {
        _shootingTimer.UpdateTimeLimit(0.25f);
        _timer.UpdateTimerLimit(20.0f);
    }
}
