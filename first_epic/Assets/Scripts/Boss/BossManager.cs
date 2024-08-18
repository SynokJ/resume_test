using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private Transform _playerTr;
    [SerializeField] private GameObject _bulletPref;

    private List<System.Type> _bossTemplates = new List<System.Type>()
    {
        typeof(MovingBoss),
        typeof(ShootingBoss)
    };

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        GameObject tempBoss = new GameObject();
        System.Type bossType = GenerateRndBoss(tempBoss);

        GenerateRenderer(tempBoss, GameConstants.BOSS_SPRITE_PATH[bossType]);
        GenerateCollider(tempBoss);
        GenerateAnimation(tempBoss, GameConstants.BOSS_ANIMATOR_PATH[bossType]);
        
        GenerateBoss(tempBoss, bossType);
    }

    private System.Type GenerateRndBoss(GameObject boss)
    {
        int size = _bossTemplates.Count;
        int id = UnityEngine.Random.Range(0, size);
        return _bossTemplates[id];
    }

    private void GenerateRenderer(GameObject boss, string path)
    {
        SpriteRenderer spriteRenderer = boss.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(path);
        spriteRenderer.material = Resources.Load<Material>("Materials/M_DefaultWall");
        spriteRenderer.material.color = Color.white;
    }

    private void GenerateAnimation(GameObject boss, string path)
    {
        Animator animator = boss.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path);
    }

    private void GenerateCollider(GameObject boss)
    {
        BoxCollider2D collider = boss.AddComponent<BoxCollider2D>();
    }

    private void GenerateBoss(GameObject boss, System.Type type)
    {
        if (type.Equals(typeof(ShootingBoss)))
        {
            ShootingBoss shooting = boss.AddComponent<ShootingBoss>();
            shooting.InitBase(_scrollbar);
            shooting.InitShootingBoss(_bulletPref, _playerTr);
            boss.name = "Shooting Boss";
        } else if(type.Equals(typeof(MovingBoss)))
        {
            MovingBoss moving = boss.AddComponent<MovingBoss>();
            moving.InitBase(_scrollbar);
            moving.InitMovingBoss();
            boss.name = "Moving Boss";
        }
    }
}
