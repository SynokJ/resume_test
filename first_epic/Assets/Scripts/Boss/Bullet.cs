using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossPlayer player = collision.GetComponent<BossPlayer>();
        if (player != null)
            SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetVisibilityStatus(bool status)
    {
        _spriteRenderer.enabled = status;
        _circleCollider.enabled = status;
    }

    public void OnActivated(UnityEngine.Vector2 dir)
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(dir * 5.0f);
    }
}
