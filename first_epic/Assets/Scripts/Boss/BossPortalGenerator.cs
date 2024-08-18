public class BossPortalGenerator
{
    private RandomPathGenerator _randomPathGenerator;

    public BossPortalGenerator(RandomPathGenerator randomPathGenerator)
    {
        _randomPathGenerator = randomPathGenerator;
    }

    ~BossPortalGenerator()
    {
        _randomPathGenerator = null;
    }

    public void Generate()
    {
        UnityEngine.GameObject bossPortal = new UnityEngine.GameObject("Boss");
        bossPortal.transform.position = _randomPathGenerator.GetRndSpawnPosition();

        GenerateSpriteRenderer(bossPortal);
        GenerateEnterCollider(bossPortal);
        GenerateCollider(bossPortal);
        GenerateAnimator(bossPortal);

        bossPortal.AddComponent<AnimationSpriteSwitch>();
        bossPortal.AddComponent<BossPortal>();
    }

    private void GenerateSpriteRenderer(UnityEngine.GameObject portal)
    {
        UnityEngine.SpriteRenderer spriteRenderer = portal.AddComponent<UnityEngine.SpriteRenderer>();
        spriteRenderer.sprite = UnityEngine.Resources.Load<UnityEngine.Sprite>("Sprites/Maze/Elevator_Idle");
        spriteRenderer.material = UnityEngine.Resources.Load<UnityEngine.Material>("Materials/M_DefaultWall");
    }

    private void GenerateCollider(UnityEngine.GameObject portal)
    {
        UnityEngine.BoxCollider2D collider = portal.AddComponent<UnityEngine.BoxCollider2D>();
        collider.isTrigger = true;
    }

    private void GenerateEnterCollider(UnityEngine.GameObject portal)
    {
        UnityEngine.BoxCollider2D collider = portal.AddComponent<UnityEngine.BoxCollider2D>();
        collider.offset = new UnityEngine.Vector2(0.0f, -2.0f);
        collider.size = new UnityEngine.Vector2(3.0f, 1.0f);
        collider.enabled = false;
    }

    private void GenerateAnimator(UnityEngine.GameObject portal)
    {
        UnityEngine.Animator animator = portal.AddComponent<UnityEngine.Animator>();
        animator.runtimeAnimatorController = 
            UnityEngine.Resources.Load<UnityEngine.RuntimeAnimatorController>("Animations/Elevator");
    }
}
