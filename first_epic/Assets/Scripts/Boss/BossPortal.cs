using UnityEngine;

public class BossPortal : MonoBehaviour
{
    private Animator _animator;
    private Collider2D[] _colliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MazePlayer player = collision.GetComponent<MazePlayer>();
        if (player != null)
        {
            _animator.SetTrigger("on_opened");
            DeactivateTriggerZone();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MazePlayer player = collision.gameObject.GetComponent<MazePlayer>();
        if (player != null)
            Teleport();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _colliders = GetComponents<Collider2D>();
    }

    private void DeactivateTriggerZone()
    {
        int size = _colliders.Length;
        for (int i = 0; i < size; ++i)
            if (_colliders[i].isTrigger)
                _colliders[i].enabled = false;
    }

    public void ActivateEnterArea()
    {
        int size = _colliders.Length;
        for (int i = 0; i < size; ++i)
            if (!_colliders[i].isTrigger)
                _colliders[i].enabled = true;
    }

    private void Teleport()
        => UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
}
