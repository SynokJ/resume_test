using UnityEngine;

public class CoinGenerator
{
    private RandomPathGenerator _randomPathGenerator;
    private CoinsManager _coinsManager;

    public CoinGenerator(RandomPathGenerator pathGenerator, CoinsManager manager)
    {
        _randomPathGenerator = pathGenerator;
        _coinsManager = manager;
    }

    ~CoinGenerator()
    {
        _randomPathGenerator = null;
    }

    public void Generate()
    {
        UnityEngine.Vector2 pos = _randomPathGenerator.GetRndSpawnPosition();
        GenerateCoin(pos);
    }

    private void GenerateCoin(UnityEngine.Vector2 spawnPos)
    {
        UnityEngine.GameObject coin = new UnityEngine.GameObject("Coin");
        coin.transform.position = spawnPos;

        GenerateSpriteRenderer(coin);
        GenerateCollider(coin);
        GenerateLight(coin);
        
        GenerateCoin(coin);
    }

    private void GenerateSpriteRenderer(UnityEngine.GameObject coin)
    {
        UnityEngine.SpriteRenderer spriteRenderer = coin.AddComponent<UnityEngine.SpriteRenderer>();
        spriteRenderer.sprite = UnityEngine.Resources.Load<UnityEngine.Sprite>("Sprites/Wall/Default Square");
        spriteRenderer.material = UnityEngine.Resources.Load<UnityEngine.Material>("Materials/M_DefaultWall");
        spriteRenderer.color = UnityEngine.Color.blue;
    }

    private void GenerateCollider(GameObject coin)
    {
        UnityEngine.CircleCollider2D colldier = coin.AddComponent<CircleCollider2D>();
        colldier.isTrigger = true;
    }

    private void GenerateLight(GameObject coin)
    {
        UnityEngine.Rendering.Universal.Light2D light = 
            coin.AddComponent<UnityEngine.Rendering.Universal.Light2D>();

        light.lightType = UnityEngine.Rendering.Universal.Light2D.LightType.Point;
        light.pointLightInnerRadius = 0.5f;
        light.pointLightOuterRadius = 3.0f;
        light.intensity = 0.5f;
        light.color = UnityEngine.Color.yellow;
    }

    private void GenerateCoin(GameObject target)
    {
        Coin coin = target.AddComponent<Coin>();
        coin.Setup(_coinsManager);
    }
}
