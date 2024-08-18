public class ShootingBossAnimation
{
    private UnityEngine.Animator _animator;

    public ShootingBossAnimation(UnityEngine.Animator animator)
    {
        _animator = animator;
    }

    ~ShootingBossAnimation()
    {
        _animator = null;
    }

    public void PlayShootAnimation()
    {
        if (_animator != null)
            _animator.SetTrigger("on_shoot");
    }
}
