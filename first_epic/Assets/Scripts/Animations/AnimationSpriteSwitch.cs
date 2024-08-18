using UnityEngine;

public class AnimationSpriteSwitch : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwitchSpriteTo(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}