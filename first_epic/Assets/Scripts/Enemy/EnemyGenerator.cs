using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyGenerator
{
    private RandomPathGenerator _pathGenerator;

    public EnemyGenerator(RandomPathGenerator pathGenerator)
    {
        _pathGenerator = pathGenerator;
    }

    ~EnemyGenerator()
    {
        _pathGenerator = null;
    }

    public void Generate()
    {
        UnityEngine.Vector2 pos = _pathGenerator.GetRndSpawnPosition();
        GenerateEnemy(pos);
    }

    private void GenerateEnemy(UnityEngine.Vector2 spawnPosition)
    {
        UnityEngine.GameObject enemy = new UnityEngine.GameObject("Enemy");
        enemy.transform.position = spawnPosition;

        SetRenderer(enemy);
        SetAnimator(enemy);
        SetRigibody(enemy);
        SetTriggerArea(enemy);
        //SetShadowCaster(enemy);

        enemy.AddComponent<UnityEngine.BoxCollider2D>();
        enemy.AddComponent<Enemy>();
    }

    private void SetRenderer(UnityEngine.GameObject enemy)
    {
        UnityEngine.SpriteRenderer spriteRenderer = enemy.AddComponent<UnityEngine.SpriteRenderer>();
        spriteRenderer.sprite = UnityEngine.Resources.Load<UnityEngine.Sprite>("Sprites/Enemy/Zombie_Movement");
        spriteRenderer.material = UnityEngine.Resources.Load<UnityEngine.Material>("Materials/M_DefaultWall");
        spriteRenderer.material.color = Color.white;
    }

    private void SetRigibody(UnityEngine.GameObject enemy)
    {
        UnityEngine.Rigidbody2D rb = enemy.AddComponent<Rigidbody2D>();
        rb.velocity = UnityEngine.Vector2.zero;
        rb.gravityScale = 0.0f;
        rb.constraints = UnityEngine.RigidbodyConstraints2D.FreezeRotation;
    }

    private void SetShadowCaster(UnityEngine.GameObject enemy)
    {
        var shadow = enemy.AddComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
        shadow.selfShadows = true;


        var field = typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers", BindingFlags.Instance | BindingFlags.NonPublic);
        int[] val = { SortingLayer.NameToID("Floor Shadow") };
        field.SetValue(shadow, val);

        var field_path = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.Instance | BindingFlags.NonPublic);
        Vector3[] path = {
            shadow.shapePath[0] * 0.5f,
            shadow.shapePath[1] * 0.5f,
            shadow.shapePath[2] * 0.5f,
            shadow.shapePath[3] * 0.5f
        };
        field_path.SetValue(shadow, path);
    }

    private void SetTriggerArea(UnityEngine.GameObject enemy)
    {
        UnityEngine.CircleCollider2D area = enemy.AddComponent<UnityEngine.CircleCollider2D>();
        area.isTrigger = true;
        area.radius = GameConstants.ENEMY_TRIGGER_RADIUS_ORIGIN;
    }

    private void SetAnimator(UnityEngine.GameObject enemy)
    {
        UnityEngine.Animator animator = enemy.AddComponent<UnityEngine.Animator>();
        animator.runtimeAnimatorController =
            UnityEngine.Resources.Load<UnityEngine.RuntimeAnimatorController>("Animations/Zombie");
    }
}
