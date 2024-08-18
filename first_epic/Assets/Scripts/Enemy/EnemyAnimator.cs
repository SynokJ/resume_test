public class EnemyAnimator
{

    private UnityEngine.Animator _animator;

    public EnemyAnimator(UnityEngine.Animator animator)
    {
        _animator = animator;
    }

    ~EnemyAnimator()
    {
    }

    public void SetAnimationByDirection(UnityEngine.Vector2 dir)
    {
        if(dir.x < 0)
        {
            _animator.SetBool("on_right", false);
            _animator.SetBool("on_left", true);
        }
        else if (dir.x > 0)
        {
            _animator.SetBool("on_right", true);
            _animator.SetBool("on_left", false);
        }
    }

}
